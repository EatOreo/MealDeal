namespace Deal;

using Newtonsoft.Json;
using System.Collections.Generic;

public class BusinessViewModel
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("ern")]
    public string Ern { get; set; }
    
    [JsonProperty("graph_id")]
    public object GraphId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("website")]
    public string Website { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("description_markdown")]
    public string DescriptionMarkdown { get; set; }
    
    [JsonProperty("logo")]
    public string Logo { get; set; }
    
    [JsonProperty("color")]
    public string Color { get; set; }
    
    [JsonProperty("pageflip")]
    public Pageflip Pageflip { get; set; }
    
    [JsonProperty("category_ids")]
    public List<object> CategoryIds { get; set; }
    
    [JsonProperty("country")]
    public Country Country { get; set; }
    
    [JsonProperty("markets")]
    public List<Market> Markets { get; set; }
    
    [JsonProperty("favorite_count")]
    public int FavoriteCount { get; set; }
    
    [JsonProperty("facebook_page_id")]
    public object FacebookPageId { get; set; }
    
    [JsonProperty("youtube_user_id")]
    public object YoutubeUserId { get; set; }
    
    [JsonProperty("twitter_handle")]
    public object TwitterHandle { get; set; }
}

public class Pageflip
{
    [JsonProperty("logo")]
    public string Logo { get; set; }
    
    [JsonProperty("color")]
    public string Color { get; set; }
}

public class Country
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("unsubscribe_print_url")]
    public object UnsubscribePrintUrl { get; set; }
}
