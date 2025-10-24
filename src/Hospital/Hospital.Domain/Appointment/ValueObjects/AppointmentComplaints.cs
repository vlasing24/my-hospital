namespace Hospital.Domain.Appointment.ValueObjects;

public record AppointmentComplaints(string Text)
{
    public static AppointmentComplaints Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Жалобы обязательны.");

        return new AppointmentComplaints(text);
    }
}