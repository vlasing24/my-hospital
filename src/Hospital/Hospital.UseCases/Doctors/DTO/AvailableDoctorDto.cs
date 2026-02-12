namespace Hospital.UseCases.Appointments.DTO;

public record AvailableDoctorDto(
    Guid Id,
    string FullName,
    string Specialization);