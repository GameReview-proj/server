using System.Text.Json.Serialization;
using JsonProperty = Newtonsoft.Json.JsonPropertyAttribute;

namespace GameReview.DTOs.IGDB;

public class IGDBQueryResult<T>
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonProperty("count")]
    public int? Count { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonProperty("result")]
    public List<T>? Result { get; set; }
}