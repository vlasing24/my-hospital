using System.Xml.Linq;

namespace MyHospital.Domain.Patient;
public class Patient
{
    public Guid Id { get; private set; }
    public PersonName Name { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public string InsuranceNumber { get; private set; }
    public ContactInfo ContactInfo { get; private set; }
    public Address Address { get; private set; }

    private Patient(Guid id, PersonName name, DateTime dateOfBirth, Gender gender, string insuranceNumber, ContactInfo contactInfo)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        InsuranceNumber = insuranceNumber;
        ContactInfo = contactInfo;
    }

    public override string ToString()
    {
        return $"Пациент ID: {ID}, ФИО: {Name}";
    }
}