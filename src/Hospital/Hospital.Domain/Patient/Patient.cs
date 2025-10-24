using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.Utilities;

namespace Hospital.Domain.Patient;

public class Patient
{
    public PatientID Id { get; }
    public FullName FullName { get; }
    public PatientDateOfBirth BirthDate { get; }
    public PatientGender Gender { get; }
    public PatientAddress Address { get; }
    public PatientInsuranceNumber InsuranceNumber { get; }
    public PatientPhone Phone { get; }

    private Patient(
        PatientID id,
        FullName fullName,
        PatientDateOfBirth birthDate,
        PatientGender gender,
        PatientAddress address,
        PatientInsuranceNumber insuranceNumber,
        PatientPhone phone)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
        Gender = gender;
        Address = address;
        InsuranceNumber = insuranceNumber;
        Phone = phone;
    }

    public static Patient Create(
        PatientID id,
        FullName fullName,
        PatientDateOfBirth birthDate,
        PatientGender gender,
        PatientAddress address,
        PatientInsuranceNumber insuranceNumber,
        PatientPhone phone)
    {
        return new Patient(id, fullName, birthDate, gender, address, insuranceNumber, phone);
    }

    public Patient UpdateFullName(FullName newFullName)
       => Create(Id, newFullName, BirthDate, Gender, Address, InsuranceNumber, Phone);

    public Patient UpdateAddress(PatientAddress newAddress)
        => Create(Id, FullName, BirthDate, Gender, newAddress, InsuranceNumber, Phone);

    public Patient UpdatePhone(PatientPhone newPhone)
        => Create(Id, FullName, BirthDate, Gender, Address, InsuranceNumber, newPhone);

    public override bool Equals(object? obj) => obj is Patient other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}