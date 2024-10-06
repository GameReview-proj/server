﻿using GameReview.Data.DTOs.IGDB;
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
        string gamesRequestBody = $"search \"{name}\"; fields cover.image_id, {fieldsSeach};";

        var gamesFound = SendIGDBRequest<ExternalApiGame>(endpoint.Url, gamesRequestBody).Result;

        return gamesFound;
    }

    public IEnumerable<ExternalAPIGenre> GetGenres(List<string> fields)
    {
        string fieldsSeach = fields.IsNullOrEmpty() ? "*" : string.Join(", ", fields);

        if (!CheckFieldsExists("Genre", fields)) throw new BadRequestException("Campos de pesquisa inválidos");

        var endpoint = GetEndpointByName("Genre");
        string genresRequestBody = $"fields {fieldsSeach};";

        var genresFound = SendIGDBRequest<ExternalAPIGenre>(endpoint.Url, genresRequestBody).Result;

        return genresFound;
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