using Hospital.Domain.Patient.ValueObjects;

namespace Hospital.Domain.Repositories;

public interface IPatientsRepository
{
    Task<bool> IsInsuranceNumberUniqueAsync(
        PatientInsuranceNumber insuranceNumber,
        CancellationToken ct = default);

    Task AddAsync(Patient.Patient patient, CancellationToken ct = default);

    Task<Patient.Patient?> GetByIdAsync(
        Guid id,
        bool withLock = false,
        CancellationToken ct = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

    Task UpdateAsync(Patient.Patient patient, CancellationToken ct = default);

    Task RemoveAsync(Patient.Patient patient, CancellationToken ct = default);
}