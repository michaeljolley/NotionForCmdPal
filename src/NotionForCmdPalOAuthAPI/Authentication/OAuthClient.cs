using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotionForCmdPalOAuthAPI.Authentication;

internal sealed class OAuthClient
{
  internal static async Task<Uri> CreateOAuthCodeRequestUriAsync()
  {
    var notionOAuthUrl = await SecretsManager.GetSecretAsync("NotionOAuthUrl");
    var notionClientId = await SecretsManager.GetSecretAsync("NotionOAuthUrl");
    var functionUrl = await SecretsManager.GetSecretAsync("FunctionUrl");
    var redirectUrl = $"{functionUrl}/token";

    return new Uri($"{notionOAuthUrl.Value}/authorize?client_id={notionClientId.Value}&response_type=code&owner=user&redirect_uri={redirectUrl}");
  }

  //internal static async Task<Uri> CreateOAuthTokenRequestUriAsync()
  //{
  //  var redirect_uri = "cmdpalnotionext://oauth_redirect_uri/";
  //  return new Uri($"{notionAPIOAuthUrl}/authorize?client_id={client_id}&response_type=code&owner=user&redirect_uri={redirect_uri}");
  //}
}