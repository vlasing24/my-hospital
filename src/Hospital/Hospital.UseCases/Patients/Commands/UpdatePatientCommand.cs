using Hospital.UseCases.Patients.DTO;

namespace Hospital.UseCases.Patients.Commands
{
    public record UpdatePatientCommand(
        Guid Id,
        PatientNameDto? Name,
        string? Phone);
}