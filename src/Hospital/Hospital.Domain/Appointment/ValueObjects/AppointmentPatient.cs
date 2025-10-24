using Hospital.Domain.Patient.ValueObjects;

namespace Hospital.Domain.Appointment.ValueObjects;

public record AppointmentPatient(PatientID PatientId)
{
    public static AppointmentPatient Create(PatientID patientId)
    {
        if (patientId == default)
            throw new ArgumentException("ID пациента обязателен.");

        return new AppointmentPatient(patientId);
    }
}