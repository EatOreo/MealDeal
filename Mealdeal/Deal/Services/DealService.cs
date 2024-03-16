using Newtonsoft.Json;

namespace Deal;

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
    
    public async Task<OffersViewModel> GetOffersFromBusinessId(string businessId)
    {
        _client.DefaultRequestHeaders.Add("X-Api-Key", "H6MGxe");
        var response = await _client.PostAsJsonAsync("https://etilbudsavis.dk/api/squid/v4/rpc/get_offers",
            new { where = new { business_ids = new[] { businessId } } });
        var result = JsonConvert.DeserializeObject<OffersViewModel>(await response.Content.ReadAsStringAsync());
        return result;
    }
    
    public async Task<OffersViewModel> GetOfferFromBusinessName(string businessName)
    {
        var business = await GetBusiness(businessName);
        var offers = await GetOffersFromBusinessId(business.Id);
        return offers;
    }
}