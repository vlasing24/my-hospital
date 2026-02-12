using Hospital.UseCases.Appointments.DTO;

namespace Hospital.UseCases.Appointments.Exceptions;

public class TimeSlotConflictException : Exception
{
    public List<AvailableTimeSlotDto> AlternativeSlots { get; }

    public TimeSlotConflictException(
        string message,
        List<AvailableTimeSlotDto> alternativeSlots)
        : base(message)
    {
        AlternativeSlots = alternativeSlots;
    }
}