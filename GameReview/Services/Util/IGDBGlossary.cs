namespace GameReview.Services.Util;

public static class IGDBGlossary
{
    public static List<IGDBEndpoint> Endpoints { get; } = new()
    {
        new IGDBEndpoint { Name = "Game", Url = "https://api.igdb.com/v4/games", Method = "POST" }
    };
}

public class IGDBEndpoint
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Method { get; set; }
}