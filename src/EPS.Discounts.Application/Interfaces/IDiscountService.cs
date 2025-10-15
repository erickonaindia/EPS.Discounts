using EPS.Discounts.Domain.Enums;

namespace EPS.Discounts.Application.Interfaces;

public interface IDiscountService
{
    Task<bool> GenerateAsync(int count, int length, CancellationToken ct = default);
    Task<UseCodeResult> UseAsync(string code, CancellationToken ct = default);
}
