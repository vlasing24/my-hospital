namespace Hospital.UseCases.Doctors.DTO;

public record UpdateDoctorDto(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? Specialization);