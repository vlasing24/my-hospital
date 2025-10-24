namespace Hospital.Domain.Appointment.ValueObjects;

public record AppointmentDiagnosis(string Code, string Description)
{
    public static AppointmentDiagnosis Create(string code, string description)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Код диагноза обязателен.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание диагноза обязательно.");

        return new AppointmentDiagnosis(code, description);
    }
}