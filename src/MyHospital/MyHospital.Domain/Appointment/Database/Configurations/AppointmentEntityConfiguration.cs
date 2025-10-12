using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.ProjectContexts.ValueObjects;

namespace MyHospital.Domain.Appointment.Database.Configurations
{
    public sealed class AppointmentEntityConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("appointment");
            builder.HasKey(x => x.Id).HasName("pk_appointment");

            builder
                .Property(x => x.Nb)
                .HasColumnName("nb")
                .HasConversion(toDb => toDb.Value, fromDb => new Number(fromDb);

            builder.HasOne(a => a.PatientA)
                .WithOne()
                .HasForeignKey<AppointmentPatient>(ap => ap.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.DoctorA)
                .WithOne()
                .HasForeignKey<AppointmentDoctor>(ad => ad.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.ComplexProperty(a => a.DateTime, dateTimeBuilder =>
            {
                dateTimeBuilder.Property(dt => dt.Value).HasColumnName("appointment_date_time").IsRequired();
            }

            builder.Property(a => a.Complaints)
               .HasColumnName("complaints")
               .HasConversion(
                   toDb => toDb.Value,
                   fromDb => new Complaints(fromDb))
               .HasMaxLength(500)
               .IsRequired();

            builder.Property(a => a.PreliminaryDiagnosis)
                .HasColumnName("preliminary_diagnosis")
                .HasConversion(
                    toDb => toDb.Value,
                    fromDb => new PreliminaryDiagnosis(fromDb))
                .HasMaxLength(500);

            builder.ComplexProperty(
            x => x.Status,
            statusBuilder =>
            {
                statusBuilder.Property(s => s.Name).IsRequired().HasColumnName("status_name");
                statusBuilder.Property(s => s.Key).IsRequired().HasColumnName("status_code");
            }
        }
    }
}