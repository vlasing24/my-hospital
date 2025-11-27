namespace Hospital.UseCases.Patients.Commands
{
    public record UpdatePatientCommand(
        Guid Id,
        string? FirstName,
        string? MiddleName,
        string? LastName,
        string? Phone);
}