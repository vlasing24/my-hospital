using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure;
using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Extensions;

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

            if (request.Name != null)
            {
                var newFirstName = request.Name.FirstName ?? patient.FullName.FirstName;
                var newMiddleName = request.Name.MiddleName ?? patient.FullName.MiddleName;
                var newLastName = request.Name.LastName ?? patient.FullName.LastName;

                var newFullName = FullName.Create(newFirstName, newMiddleName, newLastName);
                updatedPatient = updatedPatient.UpdateFullName(newFullName);
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                var newPhone = PatientPhone.Create(request.Phone);
                updatedPatient = updatedPatient.UpdatePhone(newPhone);
            }

            PatientStorage.Patients[request.Id] = updatedPatient;

            return patient.ToResponseDto();
        }
    }
}