using EPS.Discounts.Domain.Entities;

namespace EPS.Discounts.Application.Interfaces;

public interface IDiscountRepository
{
    Task<int> BulkInsertAsync(IEnumerable<DiscountCode> codes, CancellationToken ct = default);
    Task<DiscountCode?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task MarkUsedAsync(DiscountCode entity, CancellationToken ct = default);
}
