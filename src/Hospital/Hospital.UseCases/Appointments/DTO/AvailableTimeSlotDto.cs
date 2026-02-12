namespace Hospital.UseCases.Appointments.DTO;

public record AvailableTimeSlotDto(
    DateTime StartTime,
    DateTime EndTime);