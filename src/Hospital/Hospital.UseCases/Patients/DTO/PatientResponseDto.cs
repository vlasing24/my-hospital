namespace Hospital.UseCases.Patients.DTO
{
    public class PatientResponseDto
    {
        public Guid Id { get; set; }
        public PatientNameDto FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public AddressDto Address { get; set; }
        public string InsuranceNumber { get; set; }
        public string Phone { get; set; }
    }
}