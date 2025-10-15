namespace EPS.Discounts.Application.Interfaces;

public interface IProgressNotifier
{
    Task BroadcastAsync(string jobId, int percent, string message);
}
