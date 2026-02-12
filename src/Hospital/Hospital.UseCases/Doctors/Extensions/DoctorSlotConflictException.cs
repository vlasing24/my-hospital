using Hospital.UseCases.Appointments.DTO;

namespace Hospital.UseCases.Appointments.Exceptions;

public class DoctorSlotConflictException : Exception
{
    public List<AvailableDoctorDto> AlternativeDoctors { get; }

    public DoctorSlotConflictException(
        string message,
        List<AvailableDoctorDto> alternativeDoctors)
        : base(message)
    {
        AlternativeDoctors = alternativeDoctors;
    }
}