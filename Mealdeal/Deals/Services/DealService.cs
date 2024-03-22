using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Deals;

public class DealService
{
    private readonly DealContext _dealContext;
    private readonly HttpClient _client;
    public DealService(DealContext context, HttpClient client)
    {
        _dealContext = context;
        _client = client;
    }
    
    public async Task UpdateDeals()
    {
        var stores = await _dealContext.Stores.Include(store => store.Deals).ToListAsync();
        foreach (var store in stores)
        {
            var offers = await GetOffersFromBusinessName(store.Name);
            var existingDeals = store.Deals.Select(d => d.Id).ToHashSet();
            var deals = offers
                .Where(o => !existingDeals.Contains(o.Id))
                .Select(o => 
                    new Deal {
                        Id = o.Id,
                        Name = o.Name,
                        Price = o.Price
                    })
                .ToList();
            store.Deals.AddRange(deals);
        }
        await _dealContext.SaveChangesAsync();
    }
    
    public async Task<List<Deal>> GetDealsFromStore(string storeName, int limit = -1)
    {
        var store = await _dealContext.Stores
            .Include(store => store.Deals)
            .FirstOrDefaultAsync(store => store.Name == storeName);
        if (store == null) return new List<Deal>();
        else if (limit >= 0 ) return store.Deals.Take(limit).ToList();
        else return store.Deals.ToList();
    }
    
    private async Task<BusinessViewModel> GetBusiness(string businessName)
    {
        _client.DefaultRequestHeaders.Add("X-Api-Key", "H6MGxe");
        var response = await _client.GetStringAsync($"https://etilbudsavis.dk/api/squid/v2/dealers?slug={businessName}&country_code=DK");
        var result = JsonConvert.DeserializeObject<List<BusinessViewModel>>(response);
        return result.Count > 0 ? result[0] : null;
    }
    
    private async Task<List<Offer>> GetOffersFromBusinessId(string businessId, int count, int limit, string? lastCursor = "")
    {
        _client.DefaultRequestHeaders.Add("X-Api-Key", "H6MGxe");
        var response = await _client.PostAsJsonAsync("https://etilbudsavis.dk/api/squid/v4/rpc/get_offers",
                new
                {
                    where = new { business_ids = new[] { businessId } },
                    ordered_by = new[] {"unit_price_asc"},
                    page = new { after_cursor = lastCursor, page_size = limit is > 0 and < 24 ? limit : 24 }
                });
        var result = JsonConvert.DeserializeObject<OffersViewModel>(await response.Content.ReadAsStringAsync());

        if (limit != -1 && result!.Offers.Count + count >= limit) return result.Offers.Take(limit - count).ToList();
        if (!result!.PageInfo.HasNextPage) return result.Offers;
        var nextOffers = await GetOffersFromBusinessId(businessId, count + result.Offers.Count, limit, result.PageInfo.LastCursor);
        result.Offers.AddRange(nextOffers.Where(o => result.Offers.All(offer => offer.Id != o.Id)));
        return result.Offers;
    }
    
    private async Task<List<Offer>> GetOffersFromBusinessName(string businessName, int limit = -1)
    {
        var business = await GetBusiness(businessName);
        var offers = await GetOffersFromBusinessId(business.Id, 0, limit);
        return offers;
    }
}