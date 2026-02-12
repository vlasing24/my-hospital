using Hospital.UseCases.Appointments.DTO;

namespace Hospital.UseCases.Doctors.Extensions;

public static class DoctorExtensions
{
    public static AvailableDoctorDto ToResponseDto(this Doctor doctor)
    {
        if (doctor == null)
            throw new ArgumentNullException(nameof(doctor));

        return new AvailableDoctorDto(
            doctor.Id.Value,
            $"{doctor.FullName.LastName} {doctor.FullName.FirstName} {doctor.FullName.MiddleName}".Trim(),
            doctor.Specialization.Name);
    }
}