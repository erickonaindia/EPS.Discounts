namespace EPS.Discounts.Application.Interfaces;

public interface IUnitOfWork {
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
