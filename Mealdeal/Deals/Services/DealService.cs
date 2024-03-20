using Newtonsoft.Json;

namespace Deals;

public class DealService
{
    private readonly HttpClient _client;
    public DealService(HttpClient client)
    {
        _client = client;
    }
    public async Task<BusinessViewModel> GetBusiness(string businessName)
    {
        _client.DefaultRequestHeaders.Add("X-Api-Key", "H6MGxe");
        var response = await _client.GetStringAsync($"https://etilbudsavis.dk/api/squid/v2/dealers?slug={businessName}&country_code=DK");
        var result = JsonConvert.DeserializeObject<List<BusinessViewModel>>(response);
        return result.Count > 0 ? result[0] : null;
    }
    
    private async Task<List<Offer>> GetOffersFromBusinessId(string businessId, int limit, string? lastCursor = "")
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

        if (limit != -1 && result!.Offers.Count >= limit) return result.Offers.Take(limit).ToList();
        if (!result!.PageInfo.HasNextPage) return result.Offers;
        var nextOffers = await GetOffersFromBusinessId(businessId, limit, result.PageInfo.LastCursor);
        result.Offers.AddRange(nextOffers.Where(o => result.Offers.All(offer => offer.Id != o.Id)));
        return result.Offers;
    }
    
    public async Task<List<Offer>> GetOffersFromBusinessName(string businessName, int limit = -1)
    {
        var business = await GetBusiness(businessName);
        var offers = await GetOffersFromBusinessId(business.Id, limit);
        return offers;
    }
}