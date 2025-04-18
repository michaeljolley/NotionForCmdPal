using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http.Json;

using CmdPalNotionExtension.Configuration;

namespace CmdPalNotionExtension.Authentication;

internal sealed class OAuthClient
{
  public event EventHandler<OAuthEventArgs>? AccessTokenChanged;
  private const string apiAuthUrl = "https://api.notion.com/v1/oauth";

  internal DateTime StartTime
  {
    get; private set;
  }

  private static Uri CreateOauthRequestUri()
  {
    return new Uri($"{apiAuthUrl}/authorize?client_id={OAuthConfiguration.GetClientId()}&response_type=code&owner=user&redirect_uri={OAuthConfiguration.RedirectUri}");
  }

  public void BeginOAuthRequest()
  {
    var options = new Windows.System.LauncherOptions();
    var uri = CreateOauthRequestUri();
    var browserLaunch = false;
    StartTime = DateTime.Now;

    Task.Run(async () =>
    {
      browserLaunch = await Windows.System.Launcher.LaunchUriAsync(uri, options);

      if (browserLaunch)
      {
        Debug.WriteLine($"Uri Launched - Check browser");
      }
      else
      {
        Debug.WriteLine($"Uri Launch failed");
      }
    });
  }

  public async Task HandleOAuthRedirection(Uri authorizationResponse)
  { 
    // Gets URI from navigation parameters.
    var queryString = authorizationResponse.Query;

    // Parse the query string variables into a NameValueCollection.
    var queryStringCollection = HttpUtility.ParseQueryString(queryString);

    if (!string.IsNullOrEmpty(queryStringCollection.Get("error")))
    {
      throw new UriFormatException($"OAuth authorization error: {queryStringCollection.Get("error")}");
    }

    if (string.IsNullOrEmpty(queryStringCollection.Get("code")))
    {
      throw new UriFormatException($"Malformed authorization response: {queryString}");
    }

    // Gets the Authorization code
    var code = queryStringCollection.Get("code");
    try
    {
      var tokenTask = OAuthTokenRequest(code);
      var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5));

      var completedTask = await Task.WhenAny(tokenTask, timeoutTask);

      if (completedTask == timeoutTask)
      {
        throw new InvalidOperationException("Authorization code exchange timed out.");
      }

      var token = await tokenTask;
      AccessTokenChanged?.Invoke(null, new OAuthEventArgs(token.AccessToken, token.BotId));
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException(ex.Message);
    }
  }

  private static async Task<OAuthResponse> OAuthTokenRequest(string code)
  {
    var tokenUri = new Uri($"{apiAuthUrl}/token");

    var notionAuthSecret = Encoding.UTF8.GetBytes($"{OAuthConfiguration.GetClientId()}:{OAuthConfiguration.GetClientSecret()}");
    var notionBase64Auth = Convert.ToBase64String(notionAuthSecret);

    var oauthToken = new OAuthToken(code!, OAuthConfiguration.RedirectUri);

    var request = new HttpRequestMessage(HttpMethod.Post, tokenUri)
    {
      Content = new StringContent(JsonSerializer.Serialize(oauthToken), Encoding.UTF8, "application/json")
    };

    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    request.Content.Headers.Add("Notion-Version", "2022-06-28");
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", notionBase64Auth);

    using var client = new HttpClient();
    try
    {
      var responseMessage = await client.SendAsync(request);
      responseMessage.EnsureSuccessStatusCode();

      var responseContent = await responseMessage.Content.ReadFromJsonAsync<OAuthResponse>();

      if (!responseContent!.IsSuccess)
      {
        throw new InvalidOperationException("There was a problem authenticating with Notion.");
      }

      return responseContent;
    }
    catch (HttpRequestException ex)
    {
      throw new InvalidOperationException($"Request failed: {ex.Message}");
    }
    catch (JsonException ex)
    {
      throw new InvalidOperationException($"JSON parsing failed: {ex.Message}");
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Unexpected error: {ex.Message}");
    }
  }

}