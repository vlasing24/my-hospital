using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Appointment
{
    public class AppointmentPatient
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public PatientA(string name)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя пациента не может быть пустым.");
            }

            Name = name;
        }
    }
}