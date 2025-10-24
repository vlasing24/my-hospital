namespace Hospital.Domain.Patient.ValueObjects;

public readonly record struct PatientID
{
    public Guid Value { get; }

    private PatientID(Guid value) => Value = value;

    public static PatientID New() => new(Guid.NewGuid());

    public static PatientID From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ID пациента не может быть пустым.", nameof(value));
        return new PatientID(value);
    }
}