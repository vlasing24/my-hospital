namespace MyHospital.Domain.Patient;
public class Patient
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public string InsuranceNumber { get; private set; }
    public ContactInfo ContactInfo { get; private set; }
    public Address Address { get; private set; }

    private Patient(Guid id, string fullName, DateTime dateOfBirth, Gender gender, string insuranceNumber, ContactInfo contactInfo)
    {
        Id = id;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        InsuranceNumber = insuranceNumber;
        ContactInfo = contactInfo;
    }
}