using Hospital.UseCases.Doctors.DTO;

namespace Hospital.UseCases.Doctors.Commands;

public record UpdateDoctorCommand(
    Guid Id,
    UpdateDoctorDto Data);