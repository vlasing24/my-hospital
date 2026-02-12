using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hospital.Infrastructure.Repositories;

public static class TransactionScopeExtensions
{
    public static async Task<ITransactionScope> BeginTransactionScopeAsync(
        this IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        CancellationToken ct = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(ct);
        return new TransactionScopeWrapper(transaction);
    }

    private class TransactionScopeWrapper(IDbContextTransaction transaction) : ITransactionScope
    {
        public async Task CommitAsync(CancellationToken ct = default)
        {
            await transaction.CommitAsync(ct);
        }

        public void Dispose()
        {
            transaction.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return transaction.DisposeAsync();
        }
    }
}