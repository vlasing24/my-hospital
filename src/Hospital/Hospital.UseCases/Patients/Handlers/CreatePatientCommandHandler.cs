using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure.Common;
using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Extensions;

namespace Hospital.UseCases.Patients.Handlers;

public interface ICreatePatientCommandHandler
{
    Task<PatientResponseDto> HandleAsync(CreatePatientCommand command, CancellationToken ct = default);
}

public class CreatePatientCommandHandler(
    IPatientsRepository patientsRepository,
    IUnitOfWork unitOfWork,
    ApplicationDbContext context)
    : ICreatePatientCommandHandler
{
    public async Task<PatientResponseDto> HandleAsync(CreatePatientCommand request, CancellationToken ct = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(ct);

        try
        {
            var isUnique = await patientsRepository.IsInsuranceNumberUniqueAsync(
                PatientInsuranceNumber.Create(request.InsuranceNumber),
                ct);

            if (!isUnique)
                throw new InvalidOperationException($"Пациент с номером страхового полиса {request.InsuranceNumber} уже существует");

            var id = PatientID.New();
            var fullName = FullName.Create(
                request.Name.FirstName,
                request.Name.MiddleName ?? string.Empty,
                request.Name.LastName
            );
            var birthDate = PatientDateOfBirth.Create(request.DateOfBirth);
            var gender = PatientGender.From(request.Gender);

            var address = PatientAddress.Create(
                request.Address.City,
                request.Address.Street,
                request.Address.BuildingNumber,
                request.Address.ApartmentNumber
            );
            var insurance = PatientInsuranceNumber.Create(request.InsuranceNumber);
            var phone = PatientPhone.Create(request.Phone);

            var patient = Patient.Create(id, fullName, birthDate, gender, address, insurance, phone);

            await patientsRepository.AddAsync(patient, ct);

            await unitOfWork.SaveChangesAsync(ct);

            await transaction.CommitAsync(ct);

            return patient.ToResponseDto();
        }
        catch
        {
            throw;
        }
    }
}