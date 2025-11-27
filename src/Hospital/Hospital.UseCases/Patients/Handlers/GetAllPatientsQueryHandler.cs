using Hospital.Infrastructure;
using Hospital.UseCases.Patients.DTO;
using Hospital.UseCases.Patients.Queries;

namespace Hospital.UseCases.Patients.Handlers
{
    public interface IGetAllPatientsQueryHandler
    {
        IEnumerable<PatientResponseDto> Handle(GetAllPatientsQuery query);
    }

    public class GetAllPatientsQueryHandler : IGetAllPatientsQueryHandler
    {
        public IEnumerable<PatientResponseDto> Handle(GetAllPatientsQuery query)
        {
            return PatientStorage.Patients.Values.Select(p => new PatientResponseDto
            {
                Id = p.Id.Value,
                FirstName = p.FullName.FirstName,
                MiddleName = p.FullName.MiddleName,
                LastName = p.FullName.LastName,
                DateOfBirth = p.BirthDate.Value,
                Gender = p.Gender.Code,
                City = p.Address.City,
                Street = p.Address.Street,
                BuildingNumber = p.Address.BuildingNumber,
                ApartmentNumber = p.Address.ApartmentNumber,
                InsuranceNumber = p.InsuranceNumber.Number,
                Phone = p.Phone.Number
            });
        }
    }
}