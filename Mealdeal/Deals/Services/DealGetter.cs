using Microsoft.EntityFrameworkCore;

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

            using (var scope = _services.CreateScope())
            {
                var dealContext = scope.ServiceProvider.GetRequiredService<DealContext>();
                var dealService = scope.ServiceProvider.GetRequiredService<DealService>();
                
                var stores = await dealContext.Stores.Include(store => store.Deals).ToListAsync();
                foreach (var store in stores)
                {
                    var offers = await dealService.GetOfferFromBusinessName(store.Name);
                    var existingDeals = store.Deals.Select(d => d.Id).ToHashSet();
                    var deals = offers.Select(o => new Deal
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Price = o.Price
                    }).Where(o => !existingDeals.Contains(o.Id)).ToHashSet();
                    store.Deals.UnionWith(deals);
                }
                await dealContext.SaveChangesAsync();
            }
        }, null, TimeSpan.Zero, TimeSpan.FromHours(24));
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
}