using EPS.Discounts.Application.Interfaces;
using EPS.Discounts.Domain.Entities;
using Microsoft.EntityFrameworkCore; 


namespace EPS.Discounts.Infrastructure.Repositories;

public sealed class EfDiscountRepository : IDiscountRepository
{
    private readonly DiscountDbContext _db;
    public EfDiscountRepository(DiscountDbContext db) => _db = db;
    public async Task<int> BulkInsertAsync(IEnumerable<DiscountCode> codes, CancellationToken ct = default)
    {
        await _db.AddRangeAsync(codes, ct);
        return codes.Count();
    }
    public Task<DiscountCode?> GetByCodeAsync(string code, CancellationToken ct = default) => _db.DiscountCodes.FirstOrDefaultAsync(x => x.Code == code, ct);
    public Task MarkUsedAsync(DiscountCode entity, CancellationToken ct = default)
    {
        _db.DiscountCodes.Update(entity);
        return Task.CompletedTask;
    }
}
