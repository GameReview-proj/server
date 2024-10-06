using JsonProperty = Newtonsoft.Json.JsonPropertyAttribute;
using System.Text.Json.Serialization;
using JsonIgnore = System.Text.Json.Serialization;

namespace GameReview.Data.DTOs.IGDB;

public class ExternalApiCover
{
    [JsonProperty("id")]
    public int ExternalId { get; set; }

    [JsonProperty("animated")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Animated { get; set; }

    [JsonProperty("alpha_channel")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AlphaChannel { get; set; }

    [JsonProperty("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Height { get; set; }
    
    [JsonProperty("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Width { get; set; }

    [JsonProperty("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Url { get; set; }
    
    [JsonProperty("image_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ImageId { get; set; }

    [JsonProperty("game")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int GameId { get; set; }
}