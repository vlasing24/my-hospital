using Hospital.Domain.Patient;
using Hospital.Domain.Patient.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientEntityConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");

        builder.HasKey(p => p.Id).HasName("pk_patients");

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasConversion(toDb => toDb.Value, fromDb => PatientID.From(fromDb));

        builder.OwnsOne(p => p.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(f => f.FirstName)
                           .HasColumnName("first_name")
                           .IsRequired();

            fullNameBuilder.Property(f => f.LastName)
                           .HasColumnName("last_name")
                           .IsRequired();
        });

        builder.Property(p => p.BirthDate)
            .HasColumnName("date_of_birth")
            .HasConversion(toDb => toDb.Value, fromDb => PatientDateOfBirth.Create(fromDb));

        builder.Property(p => p.Gender)
            .HasColumnName("gender")
            .HasConversion(
        toDb => toDb.Code,
        fromDb => PatientGender.From(fromDb));

        builder.OwnsOne(p => p.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.City)
                          .HasColumnName("city")
                          .IsRequired();

            addressBuilder.Property(a => a.Street)
                          .HasColumnName("street")
                          .IsRequired();

            addressBuilder.Property(a => a.BuildingNumber)
                          .HasColumnName("building_number")
                          .IsRequired();

            addressBuilder.Property(a => a.ApartmentNumber)
                          .HasColumnName("apartment_number")
                          .IsRequired();
        });

        builder.Property(p => p.InsuranceNumber)
            .HasColumnName("insurance_number")
    .HasConversion(
        toDb => toDb.Number,
        fromDb => PatientInsuranceNumber.Create(fromDb));

        builder.Property(p => p.Phone)
            .HasColumnName("phone_number")
            .HasConversion(
                toDb => toDb.Number,
                fromDb => PatientPhone.Create(fromDb));
    }
}