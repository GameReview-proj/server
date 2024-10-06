using GameReview.Data.DTOs.IGDB;
using GameReview.Data.JsonObjects;
using GameReview.Services.Exceptions;
using GameReview.Services.Util;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GameReview.Services.Impl.IGDB;

public class IGDBService(IGDBTokenService tokenService, IConfiguration configuration) : IIGDBService
{
    private readonly IGDBTokenService _tokenService = tokenService;
    private readonly IConfiguration _configuration = configuration;

    public IEnumerable<ExternalApiGame> GetGamesByName(string name, List<string> fields)
    {
        string fieldsSeach = fields.IsNullOrEmpty() ? "*" : string.Join(", ", fields);

        if (!CheckFieldsExists("Game", fields)) throw new BadRequestException("Campos de pesquisa inválidos");

        var endpoint = GetEndpointByName("Game");

        using HttpClient client = new();
        SetDefaultHeaders(client);

        string gamesRequestBody = $"search \"{name}\"; fields {fieldsSeach};";
        Console.WriteLine(gamesRequestBody);
        HttpResponseMessage IGDBResponse = client.PostAsync(endpoint.Url, new StringContent(gamesRequestBody)).Result;
        IGDBResponse.EnsureSuccessStatusCode();

        string content = IGDBResponse.Content.ReadAsStringAsync().Result;
        var gamesFound = JsonConvert.DeserializeObject<IEnumerable<ExternalApiGame>>(content, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

        return gamesFound;
    }

    public void SetDefaultHeaders(HttpClient client)
    {
        IGDBToken token = _tokenService.GetToken();

        client.DefaultRequestHeaders.Add("Client-ID", _configuration["IGDB:ClientId"]);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
    }

    public IGDBEndpoint GetEndpointByName(string name)
    {
        return IGDBGlossary
            .Endpoints
            .FirstOrDefault(e => e.Name.Equals(name))
            ?? throw new ExternalAPIException("Erro ao buscar endpoint");
    }

    public bool CheckFieldsExists(string _object, List<string> fields)
    {
        return fields.All(field => IGDBGlossary.FieldsByObject.GetValueOrDefault(_object).Contains(field));
    }
}