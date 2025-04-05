using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotionExtension.Authentication;

public sealed class OAuthClient
{
  public event EventHandler<OAuthEventArgs>? AccessTokenChanged;

  private const string notionAPIOAuthUrl = "https://api.notion.com/v1/oauth";
  private const string client_id = "1cbd872b-594c-8053-9427-00379d9229a6";

  private static Uri CreateOAuthRequestUri()
  {
    var redirect_uri = "cmdpalnotionext://oauth_redirect_uri/";
    return new Uri($"{notionAPIOAuthUrl}/authorize?client_id={client_id}&response_type=code&owner=user&redirect_uri={redirect_uri}");
  }

}