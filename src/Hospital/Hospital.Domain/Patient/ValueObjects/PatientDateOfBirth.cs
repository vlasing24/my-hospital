namespace Hospital.Domain.Patient.ValueObjects;

public record PatientDateOfBirth(DateOnly Value)
{
    public static PatientDateOfBirth Create(DateOnly date)
    {
        if (date > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Дата рождения не может быть в будущем.");

        return new PatientDateOfBirth(date);
    }
}