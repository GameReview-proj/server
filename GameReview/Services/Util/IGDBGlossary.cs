namespace GameReview.Services.Util;

public static class IGDBGlossary
{
    public static List<IGDBEndpoint> Endpoints { get; } = new()
    {
        new IGDBEndpoint { Name = "Game", Url = "https://api.igdb.com/v4/games", Method = "POST" },
        new IGDBEndpoint {Name = "Genre", Url = "https://api.igdb.com/v4/genres", Method = "POST"}
    };

    public static Dictionary<string, List<string>> FieldsByObject { get; } = new()
    {
        {
            "Game", new List<string> {
                "age_ratings", "alternative_names", "category",
                "collection", "collections", "cover",
                "dlcs", "expansions", "game_engines",
                "game_modes", "game_localizations", "genres",
                "game_modes", "hypes", "involved_companies",
                "keywords", "language_supports", "multiplayer_modes",
                "player_perspectives", "ports", "rating",
                "remakes", "remasters", "similar_games",
                "status", "slug", "storyline",
                "summary", "updated_at", "url",
                "version_title", "websites", "videos",
                "name"
            }
        },
        {
            "Genre", new List<string>
            {
                "name", "slug", "url"
            }
        }
    };
}

public class IGDBEndpoint
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Method { get; set; }
}