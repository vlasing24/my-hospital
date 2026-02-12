using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.Domain.Utilities;
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
    IUnitOfWork unitOfWork)
    : ICreatePatientCommandHandler
{
    public async Task<PatientResponseDto> HandleAsync(CreatePatientCommand request, CancellationToken ct = default)
    {
        var isUnique = await patientsRepository.IsInsuranceNumberUniqueAsync(
            PatientInsuranceNumber.Create(request.InsuranceNumber),
            ct);

        if (!isUnique)
            throw new InvalidOperationException($"Пациент с номером страхового полиса {request.InsuranceNumber} уже существует");

        var patient = Patient.Create(
            PatientID.New(),
            FullName.Create(
                request.Name.FirstName,
                request.Name.MiddleName ?? string.Empty,
                request.Name.LastName),
            PatientDateOfBirth.Create(request.DateOfBirth),
            PatientGender.From(request.Gender),
            PatientAddress.Create(
                request.Address.City,
                request.Address.Street,
                request.Address.BuildingNumber,
                request.Address.ApartmentNumber),
            PatientInsuranceNumber.Create(request.InsuranceNumber),
            PatientPhone.Create(request.Phone));

        await patientsRepository.AddAsync(patient, ct);

        await unitOfWork.SaveChangesAsync(ct);

        return patient.ToResponseDto();
    }
}