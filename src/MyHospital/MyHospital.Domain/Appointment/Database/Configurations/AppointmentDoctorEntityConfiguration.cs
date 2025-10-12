using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHospital.Domain.Appointment;
using MyHospital.Domain.Doctor;

namespace MyHospital.Domain.Appointment.Database.Configurations
{
    public class AppointmentDoctorEntityConfiguration : IEntityTypeConfiguration<AppointmentDoctor>
    {
        public void Configure(EntityTypeBuilder<AppointmentDoctor> builder)
        {
            builder.ToTable("appointment_doctor");
            builder.HasKey(ad => ad.Id);

            builder.Property(ad => ad.Id)
                .HasColumnName("id");

            builder.HasOne(ad => ad.Doctor)
                .WithMany()
                .HasForeignKey(ad => ad.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ad => ad.DoctorId).HasColumnName("doctor_id").IsRequired();
        }
    }
}