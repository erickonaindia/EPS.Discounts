using EPS.Discounts.Application.Interfaces;
using EPS.Discounts.Domain.Entities;

namespace EPS.Discounts.Tests.Fakes;

public sealed class InMemoryDiscountRepository : IDiscountRepository
{
    private readonly Dictionary<string, DiscountCode> _store = new(StringComparer.Ordinal);

    public Task<int> BulkInsertAsync(IEnumerable<DiscountCode> codes, CancellationToken ct = default)
    {
        int inserted = 0;
        foreach (var c in codes)
        {
            if (_store.ContainsKey(c.Code))
                throw new Exception("duplicate");
            
            _store[c.Code] = c; inserted++;
        }
        return Task.FromResult(inserted);
    }

    public Task<DiscountCode?> GetByCodeAsync(string code, CancellationToken ct = default)
        => Task.FromResult(_store.TryGetValue(code, out var v) ? v : null);

    public Task MarkUsedAsync(DiscountCode entity, CancellationToken ct = default) => Task.CompletedTask;
}