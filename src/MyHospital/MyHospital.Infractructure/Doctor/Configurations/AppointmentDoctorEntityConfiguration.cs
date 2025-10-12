using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MMyHospital.Infrastructure.Doctor.Configurations
{
    public class AppointmentDoctorEntityConfiguration : IEntityTypeConfiguration<AppointmentDoctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors");

            builder.HasKey(d => d.Id).HasName("pk_doctors");

            builder.Property(d => d.Id).HasColumnName("id").HasConversion(toDb => toDb.Value, fromDb => DoctorID.Create(fromDb));
            builder.Property(d => d.Account).HasColumnName("account").HasConversion(toDb => toDb.Value, fromDb => Account.Create(fromDb));
            builder.Property(d => d.Category).HasColumnName("category").HasConversion(toDb => toDb.Value, fromDb => Category.Create(fromDb));
            builder.Property(d => d.Specialization).HasColumnName("specialization").HasConversion(toDb => toDb.Value, fromDb => Specialization.Create(fromDb));
        }
    }
}