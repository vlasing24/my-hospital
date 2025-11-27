using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure;
using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.DTO;

namespace Hospital.UseCases.Patients.Handlers
{
    public interface IUpdatePatientCommandHandler
    {
        PatientResponseDto Handle(UpdatePatientCommand command);
    }

    public class UpdatePatientCommandHandler : IUpdatePatientCommandHandler
    {
        public PatientResponseDto Handle(UpdatePatientCommand request)
        {
            if (!PatientStorage.Patients.TryGetValue(request.Id, out Patient? patient))
                throw new InvalidOperationException($"Не найден пациент с ID: {request.Id}");

            var updatedPatient = patient;

            if (!string.IsNullOrEmpty(request.FirstName) || !string.IsNullOrEmpty(request.MiddleName) || !string.IsNullOrEmpty(request.LastName))
            {
                var newFirstName = request.FirstName ?? patient.FullName.FirstName;
                var newMiddleName = request.MiddleName ?? patient.FullName.MiddleName;
                var newLastName = request.LastName ?? patient.FullName.LastName;

                var newFullName = FullName.Create(newFirstName, newMiddleName, newLastName);
                updatedPatient = updatedPatient.UpdateFullName(newFullName);
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                var newPhone = PatientPhone.Create(request.Phone);
                updatedPatient = updatedPatient.UpdatePhone(newPhone);
            }

            PatientStorage.Patients[request.Id] = updatedPatient;

            return new PatientResponseDto
            {
                Id = updatedPatient.Id.Value,
                FirstName = updatedPatient.FullName.FirstName,
                MiddleName = updatedPatient.FullName.MiddleName,
                LastName = updatedPatient.FullName.LastName,
                DateOfBirth = updatedPatient.BirthDate.Value,
                Gender = updatedPatient.Gender.Code,
                City = updatedPatient.Address.City,
                Street = updatedPatient.Address.Street,
                BuildingNumber = updatedPatient.Address.BuildingNumber,
                ApartmentNumber = updatedPatient.Address.ApartmentNumber,
                InsuranceNumber = updatedPatient.InsuranceNumber.Number,
                Phone = updatedPatient.Phone.Number
            };
        }
    }
}