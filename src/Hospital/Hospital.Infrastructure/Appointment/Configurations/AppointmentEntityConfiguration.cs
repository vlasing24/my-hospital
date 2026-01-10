using Hospital.Domain.Appointment;
using Hospital.Domain.Appointment.ValueObjects;
using Hospital.Domain.Patient.ValueObjects; 
using Hospital.Domain.Doctor.ValueObjects;  
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppointmentEntityConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments");

        builder.HasKey(a => a.Id).HasName("pk_appointments");

        builder.Property(a => a.Id)
               .HasColumnName("id")
               .HasConversion(toDb => toDb.Value, fromDb => AppointmentID.From(fromDb));

        builder.Property(a => a.Patient)
               .HasColumnName("patient_id")
               .HasConversion(
                   toDb => toDb.PatientId.Value,
                   fromDb => AppointmentPatient.Create(PatientID.From(fromDb)));

        builder.Property(a => a.AssignedDoctor)
               .HasColumnName("doctor_id")
               .HasConversion(
                   toDb => toDb.DoctorId.Value,
                   fromDb => AppointmentDoctor.Create(DoctorID.From(fromDb)));

        builder.Property(a => a.DateTime)
               .HasColumnName("date")
               .HasConversion(
                   toDb => toDb.Value,
                   fromDb => AppointmentDate.Create(fromDb))
               .HasColumnType("timestamp with time zone");

        builder.Property(a => a.Complaints)
               .HasColumnName("complaints")
               .HasConversion(
                   toDb => toDb.Text,
                   fromDb => AppointmentComplaints.Create(fromDb));

        builder.Property(a => a.PreliminaryDiagnosis)
       .HasColumnName("diagnosis")
       .HasConversion(
           toDb => toDb != null ? toDb.Description : null,
           fromDb => string.IsNullOrEmpty(fromDb) ? null : AppointmentDiagnosis.Create("", fromDb));

        builder.Property(a => a.Status)
               .HasColumnName("status")
               .HasConversion(
                   toDb => toDb.Code,
                   fromDb => AppointmentStatus.FromCode(fromDb));
    }
}