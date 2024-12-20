﻿using Newtonsoft.Json;

namespace GameReview.DTOs.JsonObjects;

public class IGDBToken()
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; set; }
    [JsonProperty("expires_in")]
    public long ExpiresIn { get; set; }
    public DateTime GeneratedDate { get; } = DateTime.Now;

    public bool EnsureTokenNotExpired()
    {
        return !string.IsNullOrEmpty(AccessToken) && GeneratedDate.AddSeconds(ExpiresIn) > DateTime.Now;
    }
}