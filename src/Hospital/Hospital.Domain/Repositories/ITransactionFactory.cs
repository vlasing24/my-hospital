namespace Hospital.Domain.Repositories;

public interface ITransactionFactory
{
    Task<ITransactionScope> BeginTransactionAsync(CancellationToken ct = default);
}