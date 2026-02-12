namespace Hospital.UseCases.Appointments.Commands;

public record RescheduleAppointmentCommand(
    Guid AppointmentId,      
    Guid DoctorId,           
    DateTime NewDateTime);  