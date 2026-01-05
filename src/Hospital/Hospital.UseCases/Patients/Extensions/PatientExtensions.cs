using Hospital.Domain.Patient;
using Hospital.UseCases.Patients.DTO;

namespace Hospital.UseCases.Patients.Extensions
{
    public static class PatientExtensions
    {
        public static PatientResponseDto ToResponseDto(this Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            return new PatientResponseDto
            {
                Id = patient.Id.Value,
                FullName = new PatientNameDto
                {
                    FirstName = patient.FullName.FirstName,
                    MiddleName = patient.FullName.MiddleName,
                    LastName = patient.FullName.LastName
                },
                DateOfBirth = patient.BirthDate.Value,
                Gender = patient.Gender.Code,
                Address = new AddressDto
                {
                    City = patient.Address.City,
                    Street = patient.Address.Street,
                    BuildingNumber = patient.Address.BuildingNumber,
                    ApartmentNumber = patient.Address.ApartmentNumber
                },
                InsuranceNumber = patient.InsuranceNumber.Number,
                Phone = patient.Phone.Number
            };
        }
    }
}