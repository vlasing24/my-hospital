using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class PatientsRepository(ApplicationDbContext context) : IPatientsRepository
{
    private readonly DbSet<Patient> _patients = context.Set<Patient>();

    public async Task<bool> IsInsuranceNumberUniqueAsync(
        PatientInsuranceNumber insuranceNumber,
        CancellationToken ct = default)
    {
        return !await _patients.AnyAsync(
            p => p.InsuranceNumber.Number == insuranceNumber.Number, ct);
    }

    public async Task AddAsync(Patient patient, CancellationToken ct = default)
    {
        await _patients.AddAsync(patient, ct);
    }

    public async Task<Patient?> GetByIdAsync(Guid id, bool withLock = false, CancellationToken ct = default)
    {
        if (!withLock)
            return await _patients.FindAsync(new object?[] { id }, ct);

        return await _patients
            .FromSqlRaw($"SELECT * FROM patients WHERE id = @p0 FOR UPDATE", id)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _patients.AnyAsync(p => p.Id.Value == id, ct);
    }

    public Task UpdateAsync(Patient patient, CancellationToken ct = default)
    {
        _patients.Update(patient);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Patient patient, CancellationToken ct = default)
    {
        _patients.Remove(patient);
        return Task.CompletedTask;
    }
}