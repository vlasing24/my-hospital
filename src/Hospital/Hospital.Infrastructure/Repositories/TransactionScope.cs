using System; // ← Для IDisposable (часто не нужно при ImplicitUsings)
using Microsoft.EntityFrameworkCore.Storage;

namespace Hospital.Infrastructure.Repositories;

public class TransactionScope : IDisposable
{
    private readonly IDbContextTransaction _transaction;

    public TransactionScope(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public void Dispose()
    {
        _transaction?.Dispose(); // ← Автоматический откат при исключении
    }
}