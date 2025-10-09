using System;

namespace MyHospital.Domain.Appointment;

public class Appointment
{
    public Number Nb { get; private set; }
    public PatientA PatientA { get; private set; }
    public DoctorA DoctorA { get; private set; }
    public DateTime DateTime { get; private set; }
    public Complaints Complaints { get; private set; }
    public PreliminaryDiagnosis PreliminaryDiagnosis { get; private set; }

    public Appointment(PatientA patient, DoctorA doctor, ValueObjects.DateTime appointmentDateTime, ValueObjects.Complaints complaints)
    {
        Nb = Guid.NewGuid();
        PatientA = patient ?? throw new ArgumentNullException(nameof(patient));
        DoctorA = doctor ?? throw new ArgumentNullException(nameof(doctor));
        DateTime = appointmentDateTime ?? throw new ArgumentNullException(nameof(appointmentDateTime));
        Complaints = complaints ?? throw new ArgumentNullException(nameof(complaints));
    }

    public void UpdatePreliminaryDiagnosis(ValueObjects.PreliminaryDiagnosis diagnosis)
    {
        PreliminaryDiagnosis = diagnosis ?? throw new ArgumentNullException(nameof(diagnosis));
    }
}
}