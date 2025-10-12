using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHospital.Domain.Database.Configurations
{
    public class AppointmentDoctorEntityConfiguration : IEntityTypeConfiguration<AppointmentDoctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors");

            builder.HasKey(d => d.Id).HasName("pk_doctors");

            builder.Property(d => d.Id).HasColumnName("id").HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(d => d.Account).HasColumnName("account").HasMaxLength(255).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(d => d.Category).HasColumnName("category").HasMaxLength(100).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(d => d.Specialization).HasColumnName("specialization").HasMaxLength(255).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
        }
    }
}