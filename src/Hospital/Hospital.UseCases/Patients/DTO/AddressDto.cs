namespace Hospital.UseCases.Patients.DTO
{
    public class AddressDto
    {
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public string ApartmentNumber { get; set; } = null!;
    }
}