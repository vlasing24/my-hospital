using Hospital.Domain.Patient.ValueObjects;

namespace Hospital.UseCases.Appointments.Commands;

public record BookAppointmentCommand(
    PatientID PatientId,
    Guid DoctorId,
    DateTime AppointmentDateTime,
    string Complaints);