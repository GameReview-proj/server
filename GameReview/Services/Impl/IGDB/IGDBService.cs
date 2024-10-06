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

    public IEnumerable<IGDBQueryResult<ExternalApiGame>> GetGamesByName(string name, List<string>? fields, int? from, int? take, List<int>? platforms)
    {
        string fieldsSeach = $"cover.id, {(fields.IsNullOrEmpty() ? "*" : string.Join(", ", fields))}";
        if (!CheckFieldsExists("Game", fields)) throw new BadRequestException("Campos de pesquisa inválidos");

        List<string> filters = [ $"name ~ *\"{name}\"*" ];

        if (!platforms.IsNullOrEmpty()) filters.Add($"platform != n & platform = ({string.Join(", ", platforms)})");

        string multiQueryBody = GenerateMultiQueryBody(new Dictionary<string, MultiQuery>
        {
            {
                "games", new("games", "Games") {
                    Fields = fieldsSeach,
                    Filters = filters,
                    From = from,
                    Take = take,
                    Pageable = true
                }
            },
            {
                "games/count", new("games/count", "Count") {
                    Filters = filters,
                    Pageable = false
                }
            }
        });

        var endpoint = GetEndpointByName("MultiQuery");

        var gamesFound = SendIGDBRequest<IGDBQueryResult<ExternalApiGame>>(endpoint.Url, multiQueryBody).Result;

        return gamesFound;
    }

    public IEnumerable<ExternalAPIGenre> GetGenres(List<string>? fields)
    {
        string fieldsSeach = fields.IsNullOrEmpty() ? "*" : string.Join(", ", fields);

        if (!CheckFieldsExists("Genre", fields)) throw new BadRequestException("Campos de pesquisa inválidos");

        var endpoint = GetEndpointByName("Genre");
        string genresRequestBody = $"fields {fieldsSeach};";

        var genresFound = SendIGDBRequest<ExternalAPIGenre>(endpoint.Url, genresRequestBody).Result;

        return genresFound;
    }

    private static string GenerateMultiQueryBody(Dictionary<string, MultiQuery> queries)
    {
        string requestBody = "";

        foreach (string key in queries.Keys)
        {
            var query = queries.GetValueOrDefault(key);

            requestBody += $"query {query.ObjectName} \"{query.Name}\" {{" +
                $"{(query.Fields != string.Empty ? $"fields {query.Fields};" : string.Empty)}" +
                $"{(!query.Filters.IsNullOrEmpty() ? $"where {string.Join("& ", query.Filters)};" : string.Empty)}" +
                $"{(query.Pageable ? $"limit {query.Take}; offset {query.From};" : string.Empty)}" +
                $"}};";
        }

        return requestBody;
    }

    private record MultiQuery(string ObjectName, string Name)
    {
        public string? Fields { get; set; } = string.Empty;
        public List<string> Filters { get; set; } = [string.Empty];
        public int? From { get; set; } = 0;
        public int? Take { get; set; } = 0;
        public bool Pageable { get; set; } = false;
    }

    private async Task<IEnumerable<T>> SendIGDBRequest<T>(string url, string requestBody)
    {
        using HttpClient client = new();
        SetDefaultHeaders(client);

        HttpResponseMessage IGDBResponse = await client.PostAsync(url, new StringContent(requestBody));
        IGDBResponse.EnsureSuccessStatusCode();

        string content = await IGDBResponse.Content.ReadAsStringAsync();
        if (content == "") return [];

        var responseFound = JsonConvert.DeserializeObject<IEnumerable<T>>(content, new JsonSerializerSettings())
            ?? throw new ExternalAPIException("Falha ao converter JSON");

        return responseFound;
    }

    private void SetDefaultHeaders(HttpClient client)
    {
        IGDBToken token = _tokenService.GetToken();

        client.DefaultRequestHeaders.Add("Client-ID", _configuration["IGDB:ClientId"]);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
    }

    private IGDBEndpoint GetEndpointByName(string name) => IGDBGlossary
        .Endpoints
        .FirstOrDefault(e => e.Name.Equals(name))
        ?? throw new ExternalAPIException("Erro ao buscar endpoint");

    private bool CheckFieldsExists(string _object, List<string> fields) => fields.All(field => IGDBGlossary
        .FieldsByObject
        .GetValueOrDefault(_object)
        .Contains(field));
}