using System;

namespace MyHospital.Domain.Appointment;

public class Appointment
{
    public Number Nb { get; private set; }
    public AppointmentPatient PatientA { get; private set; }
    public AppointmentDoctor DoctorA { get; private set; }
    public AppointmentDate DateTime { get; private set; }
    public Complaints Complaints { get; private set; }
    public PreliminaryDiagnosis PreliminaryDiagnosis { get; private set; }

    public Appointment(AppointmentPatient patient, AppointmentDoctor doctor, ValueObjects.DateTime appointmentDateTime, ValueObjects.Complaints complaints)
    {
        Nb = Guid.NewGuid();
        PatientA = patient;
        DoctorA = doctor;
        DateTime = appointmentDateTime;
        Complaints = complaints;
    }

    public void UpdatePreliminaryDiagnosis(ValueObjects.PreliminaryDiagnosis diagnosis)
    {
        PreliminaryDiagnosis = diagnosis;
    }
}