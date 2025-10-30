using System.Net;
using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;
using Hospital.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presenters.Controllers
{
    /// <summary>
    /// Контроллер для управления пациентами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        /// <summary>
        /// Создаёт нового пациента.
        /// </summary>
        /// <param name="request">Данные пациента.</param>
        /// <returns>Информация о созданном пациенте.</returns>
        [HttpPost]
        public IResult Create([FromBody] CreatePatientRequestDto request)
        {
            try
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

                var dto = new PatientResponseDto
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

                return new Envelope(dto);
            }
            catch (Exception ex)
            {
                return new Envelope(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Возвращает пациента по ID.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пациента.</param>
        /// <returns>Информация о пациенте.</returns>
        [HttpGet("{id:guid}")]
        public IResult Get([FromRoute] Guid id)
        {
            if (!PatientStorage.Patients.TryGetValue(id, out Patient? patient))
                return new Envelope(HttpStatusCode.NotFound, $"Не найден пациент с ID: {id}");

            var dto = new PatientResponseDto
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

            return new Envelope(dto);
        }

        /// <summary>
        /// Возвращает список всех пациентов.
        /// </summary>
        /// <returns>Список пациентов.</returns>
        [HttpGet]
        public IResult GetAll()
        {
            var dtos = PatientStorage.Patients.Values.Select(p => new PatientResponseDto
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

            return new Envelope(dtos);
        }

        /// <summary>
        /// Обновляет информацию о пациенте.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пациента.</param>
        /// <param name="request">Данные для обновления.</param>
        /// <returns>Информация об обновлённом пациенте.</returns>
        [HttpPut("{id:guid}")]
        public IResult Update([FromRoute] Guid id, [FromBody] UpdatePatientRequestDto request)
        {
            if (!PatientStorage.Patients.TryGetValue(id, out Patient? patient))
                return new Envelope(HttpStatusCode.NotFound, $"Не найден пациент с ID: {id}");

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

            PatientStorage.Patients[id] = updatedPatient;

            var dto = new PatientResponseDto
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

            return new Envelope(dto);
        }

        /// <summary>
        /// Удаляет пациента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пациента.</param>
        /// <returns>Статус операции.</returns>
        [HttpDelete("{id:guid}")]
        public IResult Delete([FromRoute] Guid id)
        {
            if (!PatientStorage.Patients.Remove(id))
                return new Envelope(HttpStatusCode.NotFound, $"Не найден пациент с ID: {id}");

            return new Envelope(HttpStatusCode.OK, "Patient deleted successfully.");
        }
    }

    public class PatientResponseDto
    {
        /// <summary>
        /// Уникальный идентификатор пациента.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пациента.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Отчество пациента.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Фамилия пациента.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Пол пациента (M/F).
        /// </summary>
        public string Gender { get; set; } = null!;

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; } = null!;

        /// <summary>
        /// Улица.
        /// </summary>
        public string Street { get; set; } = null!;

        /// <summary>
        /// Номер дома.
        /// </summary>
        public string BuildingNumber { get; set; } = null!;

        /// <summary>
        /// Номер квартиры.
        /// </summary>
        public string ApartmentNumber { get; set; } = null!;

        /// <summary>
        /// Номер страхового полиса.
        /// </summary>
        public string InsuranceNumber { get; set; } = null!;

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; } = null!;
    }

    public class CreatePatientRequestDto
    {
        /// <summary>
        /// Имя пациента.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Отчество пациента.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Фамилия пациента.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Пол пациента (M/F).
        /// </summary>
        public string Gender { get; set; } = null!;

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; } = null!;

        /// <summary>
        /// Улица.
        /// </summary>
        public string Street { get; set; } = null!;

        /// <summary>
        /// Номер дома.
        /// </summary>
        public string BuildingNumber { get; set; } = null!;

        /// <summary>
        /// Номер квартиры.
        /// </summary>
        public string ApartmentNumber { get; set; } = null!;

        /// <summary>
        /// Номер страхового полиса.
        /// </summary>
        public string InsuranceNumber { get; set; } = null!;

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; } = null!;
    }

    public class UpdatePatientRequestDto
    {
        /// <summary>
        /// Имя пациента.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Отчество пациента.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Фамилия пациента.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string? Phone { get; set; }
    }
    public class Envelope : IResult
    {
        public int Status { get; }
        public object? Data { get; }
        public string? Error { get; }

        public Envelope(HttpStatusCode statusCode, object? data = null, string? error = null)
        {
            Status = (int)statusCode;
            Data = data;
            Error = error;
        }

        public Envelope(object data)
        {
            Status = 200;
            Error = null;
            Data = data;
        }

        public Envelope(HttpStatusCode statusCode, string error)
        {
            Status = (int)statusCode;
            Error = error;
            Data = null;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = Status;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsJsonAsync(this);
        }
    }
}