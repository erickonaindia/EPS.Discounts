using Grpc.Core;
using EPS.Discounts.Application.Interfaces;
using NB.Discounts.Api;

namespace EPS.Discounts.API;

public sealed class DiscountGrpcService : DiscountService.DiscountServiceBase
{
    private readonly IDiscountService _svc;
    private readonly IProgressNotifier _notifier;
    public DiscountGrpcService(IDiscountService svc, IProgressNotifier notifier)
    {
        _svc = svc;
        _notifier = notifier;
    }

    public override async Task<GenerateResponse> Generate(GenerateRequest request, ServerCallContext ctx)
    {
        var jobId = Guid.NewGuid().ToString("N");
        await _notifier.BroadcastAsync(
            jobId,
            0,
            $"Starting: {request.Count} codes len={request.Length}");

        var ok = await _svc.GenerateAsync(
            (int)request.Count,
            (int)request.Length,
            ctx.CancellationToken);
        await _notifier.BroadcastAsync(
            jobId,
            ok ? 100 : 0,
            ok ? "Done" : "Failed");

        return new GenerateResponse { Result = ok };
    }
    public override async Task<UseCodeResponse> UseCode(UseCodeRequest req, ServerCallContext ctx)
    {
        var res = await _svc.UseAsync(
            req.Code,
            ctx.CancellationToken);
        return new UseCodeResponse { ResultCode = (uint)res };
    }
}
