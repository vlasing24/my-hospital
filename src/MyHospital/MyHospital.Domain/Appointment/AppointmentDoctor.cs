using MyHospital.Domain.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public class AppointmentDoctor
    {
        public DoctorId Id { get; private set; }
        public PersonName Name { get; private set; }
        public Specialization Specialization { get; private set; }


        private Doctor(DoctorId id, DoctorName name, Specialization specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
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

    public class PersonName
    {
        public string Value { get; }

        public PersonName(string name, string surname, string thirdname)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("ФИО доктора не может быть пустым.");
            }
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class Specialization
    {
        public string Value { get; }

        public Specialization(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Специализация не может быть пустой.");
            }
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}