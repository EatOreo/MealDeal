using Newtonsoft.Json;

namespace Deal;

public class PageInfo
{
    [JsonProperty("last_cursor")]
    public string LastCursor { get; set; }
    
    [JsonProperty("has_next_page")]
    public bool HasNextPage { get; set; }
}

public class Image
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string Url { get; set; }
}

public class PieceCount
{
    public int From { get; set; }
    public int To { get; set; }
}

public class UnitSize
{
    public float From { get; set; }
    public float To { get; set; }
}

public class Validity
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}

public class Market
{
    public string Slug { get; set; }
    [JsonProperty("country_code")]
    public string CountryCode { get; set; }
}

public class Business
{
    [JsonProperty("__typename")]
    public string TypeName { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    
    [JsonProperty("primary_color")]
    public string PrimaryColor { get; set; }
    
    [JsonProperty("positive_logotypes")]
    public List<Image> PositiveLogotypes { get; set; }
    
    [JsonProperty("negative_logotypes")]
    public List<Image> NegativeLogotypes { get; set; }
    
    [JsonProperty("country_code")]
    public string CountryCode { get; set; }
    
    [JsonProperty("short_description")]
    public string ShortDescription { get; set; }
    
    [JsonProperty("markdown_description")]
    public string MarkdownDescription { get; set; }
    
    [JsonProperty("website_link")]
    public string WebsiteLink { get; set; }
    
    public List<Market> Markets { get; set; }
}

public class Offer
{
    [JsonProperty("__typename")]
    public string TypeName { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Image> Images { get; set; }
    
    [JsonProperty("webshop_link")]
    public string WebshopLink { get; set; }
    
    public decimal Price { get; set; }
    
    [JsonProperty("currency_code")]
    public string CurrencyCode { get; set; }
    public decimal Savings { get; set; }
    
    [JsonProperty("piece_count")]
    public PieceCount PieceCount { get; set; }
    
    [JsonProperty("unit_symbol")]
    public string UnitSymbol { get; set; }
    
    [JsonProperty("unit_size")]
    public UnitSize UnitSize { get; set; }
    public Validity Validity { get; set; }
    
    [JsonProperty("visible_from")]
    public DateTime VisibleFrom { get; set; }
    public Business Business { get; set; }
}

public class OffersViewModel
{
    [JsonProperty("page_info")]
    public PageInfo PageInfo { get; set; }
    public List<Offer> Offers { get; set; }
}
