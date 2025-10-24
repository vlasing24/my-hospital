using Hospital.Domain.Doctor.ValueObjects;

namespace Hospital.Domain.Appointment.ValueObjects;

public record AppointmentDoctor(DoctorID DoctorId)
{
    public static AppointmentDoctor Create(DoctorID doctorId)
    {
        if (doctorId == default)
            throw new ArgumentException("ID врача обязателен.");

        return new AppointmentDoctor(doctorId);
    }
}