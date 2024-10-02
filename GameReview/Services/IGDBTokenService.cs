using GameReview.Data.Adapters;
using GameReview.Data.JsonObjects;
using Newtonsoft.Json;

namespace GameReview.Services;

public class IGDBTokenService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;
    private IGDBToken _token;

    public IGDBToken GenerateToken()
    {
        Console.WriteLine(_token);

        if (_token is not null && _token.EnsureTokenNotExpired())
            return _token;

        string tokenGenerationUrl = $"https://id.twitch.tv/oauth2/token?client_id={_configuration["IGDB:ClientId"]}&client_secret={_configuration["IGDB:ClientSecret"]}&grant_type=client_credentials";
        using HttpClient client = new();

        HttpResponseMessage IGDBResponse = client.PostAsync(tokenGenerationUrl, new StringContent("")).Result;
        IGDBResponse.EnsureSuccessStatusCode();

        string content = IGDBResponse.Content.ReadAsStringAsync().Result;
        var newToken = JsonConvert.DeserializeObject<IGDBToken>(content);

        _token = newToken;

        return _token;
    }
}