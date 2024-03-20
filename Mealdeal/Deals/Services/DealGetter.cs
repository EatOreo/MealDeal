namespace Deals;

public class DealGetter : IHostedService
{
    private readonly IServiceProvider _services;
    private Timer? _timer = null;
    
    public DealGetter(IServiceProvider services)
    {
        _services = services;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async _ =>
        {
            using var scope = _services.CreateScope();
            var dealService = scope.ServiceProvider.GetRequiredService<DealService>();
                
            await dealService.UpdateDeals();
        }, null, TimeSpan.Zero, TimeSpan.FromHours(24));
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
}