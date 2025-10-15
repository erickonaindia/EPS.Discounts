using EPS.Discounts.Application.Services;
using EPS.Discounts.Domain.Enums;
using EPS.Discounts.Tests.Fakes;
using FluentAssertions;

namespace EPS.Discounts.Tests;

public class DiscountServiceTests
{
    private static DiscountService CreateSvc()
    {
        var repo = new InMemoryDiscountRepository();
        var uow = new InMemoryUnitOfWork();
        return new DiscountService(repo, uow);
    }

    [Fact]
    public async Task Generate_Should_Create_Correct_Count()
    {
        var svc = CreateSvc();
        var ok = await svc.GenerateAsync(100, 8);
        ok.Should().BeTrue();
    }

    [Fact]
    public async Task UseCode_Should_Return_NotFound_When_Unknown()
    {
        var svc = CreateSvc();
        var res = await svc.UseAsync("ABCDEFGH");
        res.Should().Be(UseCodeResult.NotFound);
    }
}
