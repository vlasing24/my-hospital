namespace Hospital.UseCases.Patients.DTO
{
    public class PatientNameDto
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
    }
}
