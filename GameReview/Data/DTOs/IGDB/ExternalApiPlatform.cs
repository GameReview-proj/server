using JsonProperty = Newtonsoft.Json.JsonPropertyAttribute;
using System.Text.Json.Serialization;
using JsonIgnore = System.Text.Json.Serialization;
using GameReview.Data.DTOs.IGDB.Enums;

namespace GameReview.Data.DTOs.IGDB;

public record ExternalApiPlatform
{
    [JsonProperty("id")]
    public int ExternalId { get; set; }

    [JsonProperty("abbreviation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Abbreviation { get; set; }

    [JsonProperty("alternative_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AlternativeName { get; set; }

    [JsonProperty("alternative_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlatformCategoryEnum? category;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CategoryName => category?.ToString();

    [JsonProperty("generation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Generation { get; set; }

    [JsonProperty("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    [JsonProperty("slug")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Slug { get; set; }
    
    [JsonProperty("summary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Summary { get; set; }
    
    [JsonProperty("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get; set; }
}