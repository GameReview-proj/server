using GameReview.Data.DTOs.IGDB;
using GameReview.Data.JsonObjects;
using GameReview.Services.Exceptions;
using GameReview.Services.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameReview.Services.IGDB;

public class IGDBService(IGDBTokenService tokenService, IConfiguration configuration)
{
    private readonly IGDBTokenService _tokenService = tokenService;
    private readonly IConfiguration _configuration = configuration;

    public IEnumerable<ExternalApiGame> GetGameByName(string name)
    {
        var endpoint = GetEndpointByName("Game");

        using HttpClient client = new();
        SetDefaultHeaders(client);

        HttpResponseMessage IGDBResponse = client.PostAsync(endpoint.Url, new StringContent($"search \"{name}\";fields name;")).Result;
        IGDBResponse.EnsureSuccessStatusCode();

        string content = IGDBResponse.Content.ReadAsStringAsync().Result;
        var gamesFound = JsonConvert.DeserializeObject<IEnumerable<ExternalApiGame>>(content);

        return gamesFound;
    }

    public void SetDefaultHeaders(HttpClient client)
    {
        IGDBToken token = _tokenService.GetToken();

        client.DefaultRequestHeaders.Add("Client-ID", configuration["IGDB:ClientId"]);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
    }

    public IGDBEndpoint GetEndpointByName(string name)
    {
        return IGDBGlossary
            .Endpoints
            .FirstOrDefault(e => e.Name.Equals(name))
            ?? throw new ExternalAPIException("Erro ao buscar endpoint");
    }
}