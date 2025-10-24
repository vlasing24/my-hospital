namespace Hospital.Domain.Doctor.ValueObjects;

public readonly record struct DoctorID
{
    public Guid Value { get; }

    private DoctorID(Guid value) => Value = value;

    public static DoctorID New() => new(Guid.NewGuid());

    public static DoctorID From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ID врача не может быть пустым.", nameof(value));
        return new DoctorID(value);
    }
}