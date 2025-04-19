using System;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Authentication;

public sealed class OAuthResponse
{
  [JsonPropertyName("access_token")]
  public string? AccessToken { get; set; }

  [JsonPropertyName("bot_id")]
  public string? BotId { get; set; }

  [JsonPropertyName("error")]
  public string? Error { get; set; }

  [JsonPropertyName("error_description")]
  public string? ErrorDescription { get; }

  [JsonIgnore]
  public bool IsSuccess => String.IsNullOrEmpty(Error);
}