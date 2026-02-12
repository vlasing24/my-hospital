using Hospital.Domain.PendingEmail;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class PendingEmailsRepository(ApplicationDbContext context) : IPendingEmailsRepository
{
    private readonly DbSet<PendingEmail> _pendingEmails = context.Set<PendingEmail>();

    public Task AddAsync(PendingEmail pendingEmail, CancellationToken ct = default)
    {
        _pendingEmails.Add(pendingEmail);
        return Task.CompletedTask;
    }
}