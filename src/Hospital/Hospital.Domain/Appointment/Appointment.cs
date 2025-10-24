using Hospital.Domain.Appointment.ValueObjects;

namespace Hospital.Domain.Appointment;

public class Appointment
{
    public AppointmentID Id { get; }
    public AppointmentPatient Patient { get; }
    public AppointmentDoctor AssignedDoctor { get; }
    public AppointmentDate DateTime { get; }
    public AppointmentComplaints Complaints { get; }
    public AppointmentDiagnosis? PreliminaryDiagnosis { get; }
    public AppointmentStatus Status { get; }

    private Appointment(
        AppointmentID id,
        AppointmentPatient patient,
        AppointmentDoctor assignedDoctor,
        AppointmentDate dateTime,
        AppointmentComplaints complaints,
        AppointmentDiagnosis? preliminaryDiagnosis,
        AppointmentStatus status)
    {
        Id = id;
        Patient = patient;
        AssignedDoctor = assignedDoctor;
        DateTime = dateTime;
        Complaints = complaints;
        PreliminaryDiagnosis = preliminaryDiagnosis;
        Status = status;
    }

    public static Appointment Create(
        AppointmentID id,
        AppointmentPatient patient,
        AppointmentDoctor assignedDoctor,
        AppointmentDate dateTime,
        AppointmentComplaints complaints,
        AppointmentDiagnosis? preliminaryDiagnosis = null)
    {
        return new Appointment(
            id, patient, assignedDoctor, dateTime, complaints, preliminaryDiagnosis, AppointmentStatus.Scheduled);
    }
}