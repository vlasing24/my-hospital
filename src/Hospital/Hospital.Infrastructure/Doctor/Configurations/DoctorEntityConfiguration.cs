using Hospital.Domain.Doctor;
using Hospital.Domain.Doctor.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorEntityConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");

        builder.HasKey(d => d.Id).HasName("pk_doctors");

        builder.Property(d => d.Id)
            .HasColumnName("id")
            .HasConversion(toDb => toDb.Value, fromDb => DoctorID.From(fromDb));

        builder.Property(d => d.Specialization)
            .HasColumnName("specialization")
            .IsRequired();
    }
}