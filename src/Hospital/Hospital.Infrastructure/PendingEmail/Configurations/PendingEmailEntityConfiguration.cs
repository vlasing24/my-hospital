using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.PendingEmail;
using Hospital.Domain.PendingEmail.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PendingEmailEntityConfiguration : IEntityTypeConfiguration<PendingEmail>
{
    public void Configure(EntityTypeBuilder<PendingEmail> builder)
    {
        builder.ToTable("pending_emails");

        builder.HasKey(e => e.Id).HasName("pk_pending_emails");

        builder.Property(e => e.Id)
               .HasColumnName("id")
               .HasConversion(toDb => toDb.Value, fromDb => PendingEmailID.From(fromDb));

        builder.Property(e => e.RecipientId)
               .HasColumnName("recipient_id")
               .HasConversion(toDb => toDb.Value, fromDb => PatientID.From(fromDb));

        builder.Property(e => e.RecipientEmail)
               .HasColumnName("recipient_email")
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(e => e.Subject)
               .HasColumnName("subject")
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(e => e.Body)
               .HasColumnName("body")
               .IsRequired()
               .HasColumnType("text");

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired()
               .HasColumnType("timestamp with time zone");
    }
}