using Hospital.Domain.Appointment;
using Hospital.Domain.Appointment.ValueObjects;
using Hospital.Domain.Doctors.ValueObjects;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class AppointmentsRepository(ApplicationDbContext context) : IAppointmentsRepository
{
    private readonly DbSet<Appointment> _appointments = context.Set<Appointment>();
    private readonly DbSet<Doctor> _doctors = context.Set<Doctor>();

    public async Task<bool> HasTimeConflictAsync(
        PatientID patientId,
        AppointmentDate dateTime,
        Guid? excludingAppointmentId = null,
        CancellationToken ct = default)
    {
        var query = _appointments
            .Where(a => a.Patient.PatientId.Value == patientId.Value
                     && a.DateTime.Value.Date == dateTime.Value.Date);

        if (excludingAppointmentId.HasValue)
            query = query.Where(a => a.Id.Value != excludingAppointmentId.Value);

        return await query.AnyAsync(ct);
    }

    public async Task<bool> HasDoctorTimeConflictAsync(
        DoctorID doctorId,
        DateTime slotStart,
        bool withLock = false,
        CancellationToken ct = default)
    {
        var slotEnd = slotStart.AddHours(1);

        if (!withLock)
        {
            return await _appointments
                .AnyAsync(a =>
                    a.AssignedDoctor.DoctorId.Value == doctorId.Value &&
                    a.DateTime.Value >= slotStart &&
                    a.DateTime.Value < slotEnd,
                    ct);
        }

        return await _appointments
            .FromSqlRaw(@"
                SELECT * FROM appointments 
                WHERE doctor_id = @p0 
                  AND date >= @p1 
                  AND date < @p2
                FOR UPDATE",
                doctorId.Value,
                slotStart,
                slotEnd)
            .AnyAsync(ct);
    }

    public async Task<List<Guid>> GetAvailableDoctorIdsAsync(
        DateTime slotStart,
        CancellationToken ct = default)
    {
        var allDoctors = await _doctors.ToListAsync(ct);
        var availableIds = new List<Guid>();

        foreach (var doctor in allDoctors)
        {
            var hasConflict = await HasDoctorTimeConflictAsync(
                doctor.Id,
                slotStart,
                withLock: false,
                ct);

            if (!hasConflict)
                availableIds.Add(doctor.Id.Value);
        }

        return availableIds;
    }

    public Task AddAsync(Appointment appointment, CancellationToken ct = default)
    {
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id, bool withLock = false, CancellationToken ct = default)
    {
        if (!withLock)
            return await _appointments.FindAsync(new object?[] { id }, ct);

        return await _appointments
            .FromSqlRaw("SELECT * FROM appointments WHERE id = @p0 FOR UPDATE", id)
            .FirstOrDefaultAsync(ct);
    }

    public Task UpdateAsync(Appointment appointment, CancellationToken ct = default)
    {
        _appointments.Update(appointment);
        return Task.CompletedTask;
    }
}