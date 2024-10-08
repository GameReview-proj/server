using JsonProperty = Newtonsoft.Json.JsonPropertyAttribute;
using System.Text.Json.Serialization;
using JsonIgnore = System.Text.Json.Serialization;
using GameReview.Data.DTOs.IGDB.Enums;

namespace GameReview.Data.DTOs.IGDB;

public record ExternalApiGame
{
    [JsonProperty("id")]
    public int ExternalId { get; set; }

    [JsonProperty("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GameCategoryEnum? Category { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string CategoryName => Category?.ToString();

    [JsonProperty("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GameStatusEnum? Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StatusName => Status?.ToString();

    [JsonProperty("age_ratings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? AgeRatings { get; set; }

    [JsonProperty("alternative_names")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? AlternativeNames { get; set; }

    [JsonProperty("collections")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Collections { get; set; }

    [JsonProperty("dlcs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Dlc { get; set; }

    [JsonProperty("expansions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Expansions { get; set; }

    [JsonProperty("game_engines")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? GameEngines { get; set; }

    [JsonProperty("game_modes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? GameModes { get; set; }

    [JsonProperty("game_localizations")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? GameLocalizations { get; set; }

    [JsonProperty("hypes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Hypes { get; set; }

    [JsonProperty("involved_companies")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? InvolvedCompanies { get; set; }

    [JsonProperty("keywords")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Keywords { get; set; }

    [JsonProperty("language_supports")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? LanguageSupports { get; set; }

    [JsonProperty("multiplayer_modes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? MultiplayerModes { get; set; }

    [JsonProperty("player_perspectives")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? PlayerPerspectives { get; set; }

    [JsonProperty("ports")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Ports { get; set; }

    [JsonProperty("rating")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Rating { get; set; }

    [JsonProperty("remakes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Remakes { get; set; }

    [JsonProperty("remasters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Remasters { get; set; }

    [JsonProperty("similar_games")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? SimilarGames { get; set; }

    [JsonProperty("slug")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Slug { get; set; }

    [JsonProperty("storyline")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Storyline { get; set; }

    [JsonProperty("summary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Summary { get; set; }

    [JsonProperty("updated_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UpdatedAt { get; set; }

    [JsonProperty("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get; set; }

    [JsonProperty("version_title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VersionTitle { get; set; }

    [JsonProperty("websites")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Websites { get; set; }

    [JsonProperty("videos")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Videos { get; set; }

    [JsonProperty("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    [JsonProperty("cover")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ExternalApiCover Cover { get; set; }

    [JsonProperty("genres")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ExternalAPIGenre> Genres { get; set; }

    [JsonProperty("platforms")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int> Platforms { get; set; }
}