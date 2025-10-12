using MyHospital.Domain.Entities.Appointment.ValueObjects;
using MyHospital.Domain.Entities.Appointment.ValueObjects.Enumerations;
using MyHospital.Domain.Utilities.Enumeration;

sing System;
using System.Collections.Generic;
using System.Linq;

namespace MyHospital.Domain.Entities.Appointment
{
    public class Appointment
    {
        public Number Nb { get; private set; }
        public AppointmentPatient PatientA { get; private set; }
        public AppointmentDoctor DoctorA { get; private set; }
        public AppointmentDate DateTime { get; private set; }
        public Complaints Complaints { get; private set; }
        public PreliminaryDiagnosis PreliminaryDiagnosis { get; private set; }
        public AppointmentStatus Status { get; private set; }

        public Appointment(AppointmentPatient patient, AppointmentDoctor doctor, ValueObjects.DateTime appointmentDateTime, ValueObjects.Complaints complaints, AppointmentStatus initialStatus)
        {
            Nb = Guid.NewGuid();
            PatientA = patient;
            DoctorA = doctor;
            DateTime = appointmentDateTime;
            Complaints = complaints;
            Status = initialStatus;
        }

        public void UpdatePreliminaryDiagnosis(PreliminaryDiagnosis diagnosis)
        {
            PreliminaryDiagnosis = diagnosis;
        }
    }
}