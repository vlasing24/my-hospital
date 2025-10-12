using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHospital.Domain.Database.Configurations
{
    public sealed class AppointmentEntityConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("appointment");

            builder.HasKey(x => x.Id).HasName("pk_appointment");

            builder.Property(a => a.PatientId).HasColumnName("patient_id").HasConversion(toDb => toDb.Value, fromDb => PatientID.Create(fromDb));
            builder.Property(a => a.DoctorId).HasColumnName("doctor_id").HasConversion(toDb => toDb.Value, fromDb => DoctorID.Create(fromDb));
            builder.Property(a => a.Date).HasColumnName("date").HasConversion(toDb => toDb.Value, fromDb => Date.Create(fromDb));
            builder.Property(a => a.Diagnosis).HasColumnName("diagnosis").HasConversion(toDb => toDb.Value, fromDb => Diagnosis.Create(fromDb));

            builder.Property(a => a.Comments).HasColumnName("complaints").HasMaxLength(MAX_LENGTH).HasColumnType("jsonb").HasConversion(toDb => toDb.Value, fromDb => Complaints.Create(fromDb));

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .HasConstraintName("fk_appointments_patient_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .HasConstraintName("fk_appointments_doctor_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}