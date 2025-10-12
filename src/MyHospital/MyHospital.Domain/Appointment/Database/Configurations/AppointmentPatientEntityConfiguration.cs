using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHospital.Domain.Appointment;
using MyHospital.Domain.Patient;

namespace MyHospital.Domain.Appointment.Database.Configurations
{
    public class AppointmentPatientEntityConfiguration : IEntityTypeConfiguration<AppointmentPatient>
    {
        public void Configure(EntityTypeBuilder<AppointmentPatient> builder)
        {
            builder.ToTable("appointment_patient");
            builder.HasKey(ap => ap.Id);

            builder.Property(ap => ap.Id)
                .HasColumnName("id");

            builder.HasOne(ap => ap.Patient)
                .WithMany()
                .HasForeignKey(ap => ap.PatientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ap => ap.PatientId).HasColumnName("patient_id").IsRequired();
        }
    }
}