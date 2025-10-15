using EPS.Discounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPS.Discounts.Infrastructure;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options) { }
    public DbSet<DiscountCode> DiscountCodes => Set<DiscountCode>();
    protected override void OnModelCreating(ModelBuilder m)
    {
        var dc = m.Entity<DiscountCode>();
        dc.ToTable("discount_codes");
        dc.HasKey(x => x.Id);
        dc.Property(x => x.Code).IsRequired().HasMaxLength(8);
        dc.HasIndex(x => x.Code).IsUnique();
        dc.Property(x => x.IsUsed).HasDefaultValue(false);
        dc.Property(x => x.CreatedUtc).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
    }
}