namespace Hospital.UseCases.Patients.DTO
{
    public class PatientResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public string ApartmentNumber { get; set; } = null!;
        public string InsuranceNumber { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}