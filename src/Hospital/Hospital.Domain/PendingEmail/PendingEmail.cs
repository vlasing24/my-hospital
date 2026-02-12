using Hospital.Domain.Patient.ValueObjects;
using Hospital.Domain.PendingEmail.ValueObjects;

namespace Hospital.Domain.PendingEmail;

public class PendingEmail
{
    public PendingEmailID Id { get; }
    public PatientID RecipientId { get; }
    public string RecipientEmail { get; }
    public string Subject { get; }
    public string Body { get; }
    public DateTime CreatedAt { get; }

    private PendingEmail(
        PendingEmailID id,
        PatientID recipientId,
        string recipientEmail,
        string subject,
        string body,
        DateTime createdAt)
    {
        Id = id;
        RecipientId = recipientId;
        RecipientEmail = recipientEmail;
        Subject = subject;
        Body = body;
        CreatedAt = createdAt;
    }

    public static PendingEmail Create(
        PendingEmailID id,
        PatientID recipientId,
        string recipientEmail,
        string subject,
        string body)
    {
        if (string.IsNullOrWhiteSpace(recipientEmail))
            throw new ArgumentException("Email получателя обязателен", nameof(recipientEmail));

        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Тема сообщения обязательна", nameof(subject));

        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException("Тело сообщения обязательно", nameof(body));

        return new PendingEmail(
            id,
            recipientId,
            recipientEmail,
            subject,
            body,
            DateTime.UtcNow);
    }
}