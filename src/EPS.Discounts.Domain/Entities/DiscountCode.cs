namespace EPS.Discounts.Domain.Entities;

public class DiscountCode
{
    public Guid Id { get; set; }
    public required string Code { get; set; } = string.Empty;
    public bool IsUsed { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UsedUtc { get; set; }
}
