namespace Hospital.UseCases.Patients.Commands
{
    public record CreatePatientCommand(
    string FirstName,
    string? MiddleName,
    string LastName,
    DateOnly DateOfBirth,
    string Gender,
    string City,
    string Street,
    string BuildingNumber,
    string ApartmentNumber,
    string InsuranceNumber,
    string Phone);
}