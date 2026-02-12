using Hospital.Domain.Appointment.ValueObjects;
using Hospital.Domain.Doctors.ValueObjects;
using Hospital.Domain.Patient.ValueObjects;

namespace Hospital.Domain.Repositories;

public interface IAppointmentsRepository
{
    Task<bool> HasTimeConflictAsync(
        PatientID patientId,
        AppointmentDate dateTime,
        Guid? excludingAppointmentId = null,
        CancellationToken ct = default);

    Task<bool> HasDoctorTimeConflictAsync(
        DoctorID doctorId,
        DateTime slotStart,
        bool withLock = false,
        CancellationToken ct = default);

    Task<List<Guid>> GetAvailableDoctorIdsAsync(
        DateTime slotStart,
        CancellationToken ct = default);

    Task AddAsync(Appointment.Appointment appointment, CancellationToken ct = default);
    Task<Appointment.Appointment?> GetByIdAsync(Guid id, bool withLock = false, CancellationToken ct = default);
    Task UpdateAsync(Appointment.Appointment appointment, CancellationToken ct = default);
}