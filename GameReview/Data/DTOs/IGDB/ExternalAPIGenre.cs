using JsonProperty = Newtonsoft.Json.JsonPropertyAttribute;
using System.Text.Json.Serialization;
using JsonIgnore = System.Text.Json.Serialization;

namespace GameReview.Data.DTOs.IGDB;

public class ExternalAPIGenre
{
    [JsonProperty("id")]
    public int ExternalId { get; set; }

    [JsonProperty("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name {get;set;}

    [JsonProperty("slug")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Slug {get;set;}

    [JsonProperty("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get;set;}
}