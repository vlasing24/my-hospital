using Hospital.Domain.Doctor.ValueObjects;
using Hospital.Domain.Utilities;

namespace Hospital.Domain.Doctor;

public class Doctor
{
    public DoctorID Id { get; }
    public FullName FullName { get; }
    public DoctorSpecialization Specialization { get; }

    private Doctor(
        DoctorID id,
        FullName fullName,
        DoctorSpecialization specialization)
    {
        Id = id;
        FullName = fullName;
        Specialization = specialization;
    }

    public static Doctor Create(
        DoctorID id,
        FullName fullName,
        DoctorSpecialization specialization)
    {
        return new Doctor(id, fullName, specialization);
    }

    public Doctor UpdateSpecialization(DoctorSpecialization newSpecialization)
        => Create(Id, FullName, newSpecialization);

    public override bool Equals(object? obj) => obj is Doctor other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}