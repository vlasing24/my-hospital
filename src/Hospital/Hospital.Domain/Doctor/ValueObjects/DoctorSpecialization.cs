namespace Hospital.Domain.Doctor.ValueObjects;

public record DoctorSpecialization(string Name)
{
    public static DoctorSpecialization Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Специализация обязательна.");

        return new DoctorSpecialization(name);
    }
}