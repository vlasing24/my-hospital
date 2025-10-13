using MyHospital.Domain.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHospital.Domain.Entities.Appointment.ValueObjects
{
    public class AppointmentPatient
    {
        public Guid Id { get; private set; }
        public Patient Patient { get; private set; }

        public AppointmentPatient(Patient patient)
        {
            Id = Guid.NewGuid();

            if (patient == null)
            {
                throw new ArgumentException("Пациент не может быть пустым.");
            }

            Patient = patient;
        }
    }
}