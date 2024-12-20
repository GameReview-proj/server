﻿using GameReview.DTOs.IGDB;
using GameReview.DTOs.JsonObjects;
using GameReview.Services.Exceptions;
using GameReview.Services.Util;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GameReview.Services.Impl.IGDB;

public class IGDBService(IGDBTokenService tokenService, IConfiguration configuration) : IIGDBService
{
    private readonly IGDBTokenService _tokenService = tokenService;
    private readonly IConfiguration _configuration = configuration;

    public IEnumerable<IGDBQueryResult<ExternalApiGame>> GetGames(string? name, List<string>? fields, int? from, int? take, List<int>? platforms, List<int>? genres)
    {
        string fieldsSearch = GenerateFieldsSearch("Game", fields, "name, cover.image_id");

        List<string> filters = [];

        if (!name.IsNullOrEmpty()) filters.Add($"name ~ *\"{name}\"*");
        if (!platforms.IsNullOrEmpty()) filters.Add($"platforms != n & platforms = {{{string.Join(", ", platforms)}}}");
        if (!genres.IsNullOrEmpty()) filters.Add($"genres != n & genres = {{{string.Join(", ", genres)}}}");

        string multiQueryBody = GenerateMultiQueryBody(new Dictionary<string, MultiQuery>
        {
            {
                "games", new("games", "Games") {
                    Fields = fieldsSearch,
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
        string fieldsSearch = GenerateFieldsSearch("Genre", fields, "*");

        var endpoint = GetEndpointByName("Genre");
        string genresRequestBody = $"fields {fieldsSearch};";

        var genresFound = SendIGDBRequest<ExternalAPIGenre>(endpoint.Url, genresRequestBody).Result;

        return genresFound;
    }

    public ExternalApiGame GetGameById(int id, List<string>? fields)
    {
        string fieldsSearch = GenerateFieldsSearch("Game", fields, "name, cover.image_id");

        var endpoint = GetEndpointByName("Game");
        string requestBody = $"fields {fieldsSearch}; where id = {id};";

        var gameFound = SendIGDBRequest<ExternalApiGame>(endpoint.Url, requestBody).Result;

        if (gameFound.IsNullOrEmpty()) throw new NotFoundException($"Jogo não encontrado com o ID {id}");

        return gameFound.First();
    }

    public IEnumerable<ExternalApiPlatform> GetPlatforms(List<string>? fields, string? name)
    {
        string fieldsSearch = GenerateFieldsSearch("Platform", fields, "*");

        string filters = "";

        if (!name.IsNullOrEmpty()) filters += $"name ~ *\"{name}\"*";

        if (!filters.IsNullOrEmpty())
        {
            filters = $"where {filters};";
        }

        var endpoint = GetEndpointByName("Platform");
        string requestBody = $"fields {fieldsSearch}; limit 215; offset 0; {filters}";

        var platformsFound = SendIGDBRequest<ExternalApiPlatform>(endpoint.Url, requestBody).Result;

        return platformsFound;
    }

    private string GenerateFieldsSearch(string _object, List<string> fields, string defaultFields)
    {
        if (!CheckFieldsExists(_object, fields)) throw new BadRequestException("Campos de pesquisa inválidos");

        return fields.IsNullOrEmpty()
            ? defaultFields
            : string.Join(", ", fields);
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

    private async Task<IEnumerable<T>> SendIGDBRequest<T>(string url, string requestBody)
    {
        using HttpClient client = new();
        SetDefaultHeaders(client);

        HttpResponseMessage IGDBResponse = await client.PostAsync(url, new StringContent(requestBody));
        string content = await IGDBResponse.Content.ReadAsStringAsync();
        if (content == "") return [];

        IGDBResponse.EnsureSuccessStatusCode();

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

    private record MultiQuery(string ObjectName, string Name)
    {
        public string? Fields { get; set; } = string.Empty;
        public List<string> Filters { get; set; } = [string.Empty];
        public int? From { get; set; } = 0;
        public int? Take { get; set; } = 0;
        public bool Pageable { get; set; } = false;
    }
}