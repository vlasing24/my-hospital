namespace Hospital.Domain.Appointment.ValueObjects;

public record AppointmentTimeSlot
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private AppointmentTimeSlot(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static AppointmentTimeSlot Create(DateTime dateTime)
    {
        var start = dateTime.Date.AddHours(dateTime.Hour);
        var end = start.AddHours(1);
        return new AppointmentTimeSlot(start, end);
    }

    public bool ConflictsWith(AppointmentTimeSlot other)
    {
        return Start < other.End && End > other.Start;
    }

    public override string ToString() => $"{Start:HH:mm}–{End:HH:mm}";
}