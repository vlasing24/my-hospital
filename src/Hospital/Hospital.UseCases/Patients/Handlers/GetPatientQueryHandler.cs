using Hospital.Domain.Patient;
using Hospital.Infrastructure;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Queries;

namespace Hospital.UseCases.Patients.Handlers
{
    public interface IGetPatientQueryHandler
    {
        PatientResponseDto Handle(GetPatientQuery query);
    }

    public class GetPatientQueryHandler : IGetPatientQueryHandler
    {
        public PatientResponseDto Handle(GetPatientQuery query)
        {
            if (!PatientStorage.Patients.TryGetValue(query.Id, out Patient? patient))
                throw new InvalidOperationException($"Не найден пациент с ID: {query.Id}");

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