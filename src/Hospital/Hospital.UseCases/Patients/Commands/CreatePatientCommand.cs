using Hospital.UseCases.Patients.DTO;

namespace Hospital.UseCases.Patients.Commands
{
    public record CreatePatientCommand(
    PatientNameDto Name,
    DateOnly DateOfBirth,
    string Gender,
    AddressDto Address,
    string InsuranceNumber,
    string Phone);
}