using EPS.Discounts.API.Hubs;
using EPS.Discounts.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EPS.Discounts.API.Services;

public sealed class ProgressNotifier : IProgressNotifier
{
    private readonly IHubContext<ProgressHub> _hub;
    public ProgressNotifier(IHubContext<ProgressHub> hub) => _hub = hub;
    public Task BroadcastAsync(
        string jobId,
        int percent,
        string message) => _hub.Clients.All.SendAsync(
            "progress",
            new {
                jobId,
                percent,
                message,
                at = DateTime.UtcNow 
            });
}
