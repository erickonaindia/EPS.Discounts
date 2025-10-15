using EPS.Discounts.Application.Interfaces;

namespace EPS.Discounts.Tests.Fakes;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => Task.FromResult(0);
}
