using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure.Common;
using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Extensions;

namespace Hospital.UseCases.Patients.Handlers;

public interface IUpdatePatientCommandHandler
{
    Task<PatientResponseDto> HandleAsync(
        UpdatePatientCommand command,
        CancellationToken ct = default);
}

public class UpdatePatientCommandHandler(
    IPatientsRepository patientsRepository,
    IUnitOfWork unitOfWork,
    ApplicationDbContext context)
    : IUpdatePatientCommandHandler
{
    public async Task<PatientResponseDto> HandleAsync(
        UpdatePatientCommand command,
        CancellationToken ct = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(ct);

        try
        {
            var patient = await patientsRepository.GetByIdAsync(
                command.Id,
                withLock: true,
                ct);

            if (patient == null)
                throw new InvalidOperationException($"Пациент с ID {command.Id} не найден");

            var updatedPatient = patient;

            if (command.Name != null)
            {
                var newFullName = FullName.Create(
                    command.Name.FirstName ?? patient.FullName.FirstName,
                    command.Name.MiddleName ?? patient.FullName.MiddleName,
                    command.Name.LastName ?? patient.FullName.LastName);

                updatedPatient = updatedPatient.UpdateFullName(newFullName);
            }

            if (!string.IsNullOrEmpty(command.Phone))
            {
                var newPhone = PatientPhone.Create(command.Phone);
                updatedPatient = updatedPatient.UpdatePhone(newPhone);
            }

            await patientsRepository.UpdateAsync(updatedPatient, ct);

            await unitOfWork.SaveChangesAsync(ct);

            await transaction.CommitAsync();

            return updatedPatient.ToResponseDto();
        }
        catch
        {
            throw;
        }
    }
}