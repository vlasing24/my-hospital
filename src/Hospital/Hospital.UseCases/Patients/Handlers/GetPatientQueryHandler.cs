using Hospital.Domain.Patient;
using Hospital.Infrastructure;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Extensions;
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

            return patient.ToResponseDto();
        }
    }
}