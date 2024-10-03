using Newtonsoft.Json;

namespace GameReview.Data.DTOs.IGDB;

public record ExternalApiGame
{
    [JsonProperty("id")]
    public int ExternalId { get; set; }
    public string? Name { get; set; }
}