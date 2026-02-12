using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class DoctorsRepository(ApplicationDbContext context) : IDoctorsRepository
{
    private readonly DbSet<Doctor> _doctors = context.Set<Doctor>();

    public async Task<Doctor?> GetByIdAsync(Guid id, bool withLock = false, CancellationToken ct = default)
    {
        if (!withLock)
            return await _doctors.FindAsync(new object?[] { id }, ct);

        return await _doctors
            .FromSqlRaw("SELECT * FROM doctors WHERE id = @p0 FOR UPDATE", id)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _doctors.AnyAsync(d => d.Id.Value == id, ct);
    }

    public Task UpdateAsync(Doctor doctor, CancellationToken ct = default)
    {
        _doctors.Update(doctor);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Doctor doctor, CancellationToken ct = default)
    {
        _doctors.Remove(doctor);
        return Task.CompletedTask;
    }
}