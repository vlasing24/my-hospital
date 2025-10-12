using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHospital.Domain.Domain.Entities.Patient;
using MyHospital.Domain.Domain.Entities.Patient.ValueObjects;

namespace MyHospital.Infrastructure.Patient.Configurations
{
    public class AppointmentPatientEntityConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");

            builder.HasKey(p => p.Id).HasName("pk_patients");

            builder.Property(p => p.Id).HasColumnName("id").HasConversion(toDb => toDb.Value, fromDb => PatientID.Create(fromDb));
            builder.Property(p => p.Address).HasColumnName("address").IsRequired().HasConversion(toDb => toDb.Value, fromDb => Address.Create(fromDb));
            builder.Property(p => p.ContactInfo).HasColumnName("contact_info").HasConversion(toDb => toDb.Value, fromDb => ContactInfo.Create(fromDb));
            builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth").HasConversion(toDb => toDb.Value, fromDb => DateOfBirth.Create(fromDb));
            builder.Property(p => p.Gender).HasColumnName("gender").HasConversion(toDb => toDb.Value, fromDb => Gender.Create(fromDb));
            builder.Property(p => p.InsuranceNumber).HasColumnName("insurance_number").HasConversion(toDb => toDb.Value, fromDb => InsuranceNumber.Create(fromDb));
        }
    }
}