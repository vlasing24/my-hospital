using Hospital.Domain.Repositories; 
using Hospital.UseCases.Patients.Commands;

namespace Hospital.UseCases.Patients.Handlers;

public interface IDeletePatientCommandHandler
{
    Task HandleAsync(DeletePatientCommand command, CancellationToken ct = default);
}

public class DeletePatientCommandHandler(
    IPatientsRepository patientsRepository,
    IUnitOfWork unitOfWork,
    ITransactionFactory transactionFactory)
    : IDeletePatientCommandHandler
{
    public async Task HandleAsync(DeletePatientCommand command, CancellationToken ct = default)
    {
        await using var transaction = await transactionFactory.BeginTransactionAsync(ct);

        try
        {
            var patient = await patientsRepository.GetByIdAsync(
                command.Id,
                withLock: true,
                ct);

            if (patient == null)
                throw new InvalidOperationException($"Пациент с ID {command.Id} не найден");

            await patientsRepository.RemoveAsync(patient, ct);
            await unitOfWork.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch
        {
            throw;
        }
    }
}