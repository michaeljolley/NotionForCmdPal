using System.Text.Json.Serialization;

namespace NotionForCmdPalOAuthAPI.Authentication;

public sealed class OAuthToken
{
  [JsonPropertyName("grant_type")]
  public string GrantType { get; } = "authorization_code";

  [JsonPropertyName("code")]
  public string Code { get; }

  [JsonPropertyName("redirect_uri")]
  public string RedirectUrl { get; }

  public OAuthToken(string code, string redirectUrl)
  {
    Code = code;
    RedirectUrl = redirectUrl;
  }
}