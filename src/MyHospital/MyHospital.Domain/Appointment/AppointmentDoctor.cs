using MyHospital.Domain.Doctor;
using System;

namespace MyHospital.Domain.Appointment
{
    public class AppointmentDoctor
    {
        public DoctorId Id { get; private set; }
        public Doctor Doctor { get; private set; }

        private AppointmentDoctor(DoctorId id, Doctor doctor)
        {
            Id = id;
            Doctor = doctor;
        }

        public static AppointmentDoctor Create(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentException("Доктор не может быть пустым.");
            }

            return new AppointmentDoctor(new DoctorId(Guid.NewGuid()), doctor);
        }
    }

    public readonly struct DoctorId
    {
        public Guid Value { get; }

        public DoctorId(Guid value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}