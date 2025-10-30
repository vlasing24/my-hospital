using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;

namespace Hospital.Infrastructure;

public static class PatientStorage
{
    public static readonly Dictionary<Guid, Patient> Patients = [];

    static PatientStorage()
    {
        if (Patients.Count > 0) return;

        // Пациент 1
        var patient1Id = PatientID.From(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        var patient1 = Patient.Create(
            patient1Id,
            FullName.Create("Иван", "Иванович", "Иванов"),
            PatientDateOfBirth.Create(new DateOnly(1985, 6, 15)),
            PatientGender.Male,
            PatientAddress.Create("Москва", "Тверская", "10", "50"),
            PatientInsuranceNumber.Create("12345678901"),
            PatientPhone.Create("+79991234567")
        );

        // Пациент 2
        var patient2Id = PatientID.From(Guid.Parse("22222222-2222-2222-2222-222222222222"));
        var patient2 = Patient.Create(
            patient2Id,
            FullName.Create("Мария", "Сергеевна", "Петрова"),
            PatientDateOfBirth.Create(new DateOnly(1990, 12, 20)),
            PatientGender.Female,
            PatientAddress.Create("Санкт-Петербург", "Невский проспект", "25", "101"),
            PatientInsuranceNumber.Create("23456789012"),
            PatientPhone.Create("+79876543210")
        );

        // Пациент 3
        var patient3Id = PatientID.From(Guid.Parse("33333333-3333-3333-3333-333333333333"));
        var patient3 = Patient.Create(
            patient3Id,
            FullName.Create("Алексей", "Николаевич", "Сидоров"),
            PatientDateOfBirth.Create(new DateOnly(1978, 3, 8)),
            PatientGender.Male,
            PatientAddress.Create("Новосибирск", "Красный проспект", "1", "1"),
            PatientInsuranceNumber.Create("34567890123"),
            PatientPhone.Create("+79123456789")
        );

        Patients.Add(patient1.Id.Value, patient1);
        Patients.Add(patient2.Id.Value, patient2);
        Patients.Add(patient3.Id.Value, patient3);
    }
}