namespace Hospital.Domain.Repositories;

public interface IPendingEmailsRepository
{
    Task AddAsync(PendingEmail.PendingEmail pendingEmail, CancellationToken ct = default);
}