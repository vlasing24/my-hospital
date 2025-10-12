using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHospital.Domain.Database.Configurations
{
    public class AppointmentPatientEntityConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");

            builder.HasKey(p => p.Id).HasName("pk_patients");

            builder.Property(p => p.Id).HasColumnName("id").HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(p => p.Address).HasColumnName("address").IsRequired().HasMaxLength(255).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(p => p.ContactInfo).HasColumnName("contact_info").HasMaxLength(255).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth").HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(p => p.Gender).HasColumnName("gender").HasMaxLength(50).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
            builder.Property(p => p.InsuranceNumber).HasColumnName("insurance_number").HasMaxLength(100).HasConversion(toDb => toDb.Value, fromDb => ProjectDescription.Create(fromDb));
        }
    }
}