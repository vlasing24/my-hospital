namespace Hospital.Domain.Repositories;
public interface ITransactionScope : IDisposable, IAsyncDisposable
{
    Task CommitAsync(CancellationToken ct = default);
}