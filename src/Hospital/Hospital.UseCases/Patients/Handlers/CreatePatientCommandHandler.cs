using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure;
using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Extensions;

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
            var fullName = FullName.Create(
                request.Name.FirstName,
                request.Name.MiddleName ?? string.Empty,
                request.Name.LastName
            );
            var birthDate = PatientDateOfBirth.Create(request.DateOfBirth);
            var gender = PatientGender.From(request.Gender);

            var address = PatientAddress.Create(
                request.Address.City,
                request.Address.Street,
                request.Address.BuildingNumber,
                request.Address.ApartmentNumber
            );
            var insurance = PatientInsuranceNumber.Create(request.InsuranceNumber);
            var phone = PatientPhone.Create(request.Phone);

            var patient = Patient.Create(id, fullName, birthDate, gender, address, insurance, phone);
            PatientStorage.Patients.Add(patient.Id.Value, patient);

            return patient.ToResponseDto();
        }
    }
}