namespace Hospital.Domain.Appointment.ValueObjects;

public record AppointmentDate(DateTime Value)
{
    public static AppointmentDate Create(DateTime dateTime)
    {
        if (dateTime < DateTime.UtcNow.AddMinutes(-5))
            throw new ArgumentException("Дата приёма не может быть в прошлом.");

        return new AppointmentDate(dateTime);
    }
}