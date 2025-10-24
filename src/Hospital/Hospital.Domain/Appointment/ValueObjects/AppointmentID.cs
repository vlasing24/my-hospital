namespace Hospital.Domain.Appointment.ValueObjects;

public readonly record struct AppointmentID
{
    public Guid Value { get; }

    private AppointmentID(Guid value) => Value = value;

    public static AppointmentID New() => new(Guid.NewGuid());

    public static AppointmentID From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ID приёма не может быть пустым.", nameof(value));
        return new AppointmentID(value);
    }
}