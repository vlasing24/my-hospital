namespace Hospital.Domain.Repositories;

public interface IDoctorsRepository
{
    Task<Doctor?> GetByIdAsync(Guid id, bool withLock = false, CancellationToken ct = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

    Task UpdateAsync(Doctor doctor, CancellationToken ct = default);

    Task RemoveAsync(Doctor doctor, CancellationToken ct = default);
}