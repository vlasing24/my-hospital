using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hospital.Infrastructure.Repositories;

public class TransactionFactory(ApplicationDbContext context) : ITransactionFactory
{
    public async Task<ITransactionScope> BeginTransactionAsync(CancellationToken ct = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(ct);
        return new TransactionScope(transaction);
    }

    private class TransactionScope(IDbContextTransaction transaction) : ITransactionScope
    {
        public async Task CommitAsync(CancellationToken ct = default)
        {
            await transaction.CommitAsync(ct);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            return transaction.DisposeAsync();
        }
    }
}