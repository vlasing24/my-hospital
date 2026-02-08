using Hospital.Domain.Repositories;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Extensions;
using Hospital.UseCases.Patients.Queries;

namespace Hospital.UseCases.Patients.Handlers;

public interface IGetPatientQueryHandler
{
    Task<PatientResponseDto> HandleAsync(GetPatientQuery query, CancellationToken ct = default);
}

public class GetPatientQueryHandler(IPatientsRepository patientsRepository)
    : IGetPatientQueryHandler
{
    public async Task<PatientResponseDto> HandleAsync(GetPatientQuery query, CancellationToken ct = default)
    {
        var patient = await patientsRepository.GetByIdAsync(
            query.Id,
            withLock: false,
            ct: ct);

        if (patient == null)
            throw new InvalidOperationException($"Не найден пациент с ID: {query.Id}");

        return patient.ToResponseDto();
    }
}