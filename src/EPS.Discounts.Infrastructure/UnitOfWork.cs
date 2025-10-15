using EPS.Discounts.Application.Interfaces;

namespace EPS.Discounts.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DiscountDbContext _db;
    public UnitOfWork(DiscountDbContext db) => _db = db;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}