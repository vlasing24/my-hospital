using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure;
using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.DTO;

namespace Hospital.UseCases.Patients.Handlers
{
    public interface ICreatePatientCommandHandler
    {
        PatientResponseDto Handle(CreatePatientCommand command);
    }

    public class CreatePatientCommandHandler : ICreatePatientCommandHandler
    {
        public PatientResponseDto Handle(CreatePatientCommand request)
        {
            var id = PatientID.New();
            var fullName = FullName.Create(request.FirstName, request.MiddleName ?? string.Empty, request.LastName);
            var birthDate = PatientDateOfBirth.Create(request.DateOfBirth);
            var gender = PatientGender.From(request.Gender);
            var address = PatientAddress.Create(request.City, request.Street, request.BuildingNumber, request.ApartmentNumber);
            var insurance = PatientInsuranceNumber.Create(request.InsuranceNumber);
            var phone = PatientPhone.Create(request.Phone);

            var patient = Patient.Create(id, fullName, birthDate, gender, address, insurance, phone);
            PatientStorage.Patients.Add(patient.Id.Value, patient);

            return new PatientResponseDto
            {
                Id = patient.Id.Value,
                FirstName = patient.FullName.FirstName,
                MiddleName = patient.FullName.MiddleName,
                LastName = patient.FullName.LastName,
                DateOfBirth = patient.BirthDate.Value,
                Gender = patient.Gender.Code,
                City = patient.Address.City,
                Street = patient.Address.Street,
                BuildingNumber = patient.Address.BuildingNumber,
                ApartmentNumber = patient.Address.ApartmentNumber,
                InsuranceNumber = patient.InsuranceNumber.Number,
                Phone = patient.Phone.Number
            };
        }
    }
}