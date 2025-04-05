using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Web;

namespace NotionForCmdPalOAuthAPI.Authentication;

internal sealed class OAuthClient
{
  internal static async Task<Uri> CreateOAuthCodeRequestUriAsync()
  {
    var notionOAuthUrl = await SecretsManager.GetSecretAsync("NotionOAuthUrl");
    var notionClientId = await SecretsManager.GetSecretAsync("NotionClientId");
    var functionUrl = await SecretsManager.GetSecretAsync("FunctionUrl");
    var redirectUrl = HttpUtility.HtmlEncode($"{functionUrl.Value}/api/token");

    return new Uri($"{notionOAuthUrl.Value}/authorize?client_id={notionClientId.Value}&response_type=code&owner=user&redirect_uri={redirectUrl}");
  }

  public static async Task<IActionResult> HandleOAuthRedirection(Uri response)
  {
    var errorResult = new ContentResult() { StatusCode = 500, ContentType = "plain/text", Content = "There was a problem authenticating with Notion. Please try again later." };

    var queryString = response.Query;
    var queryStringCollection = System.Web.HttpUtility.ParseQueryString(queryString);

    if (queryStringCollection["error"] != null)
    {
      // Handle error
      Debug.WriteLine($"Error: {queryStringCollection["error"]}");
      return errorResult;
    }

    if (queryStringCollection["code"] == null)
    {
      Debug.WriteLine("No code found in the response.");
      return errorResult;
    }

    // Handle success
    var code = queryStringCollection["code"];
    Debug.WriteLine($"Code: {code}");

    // Use the code to get the access token
    var notionOAuthUrl = await SecretsManager.GetSecretAsync("NotionOAuthUrl");
    var notionClientId = await SecretsManager.GetSecretAsync("NotionClientId");
    var notionClientSecret = await SecretsManager.GetSecretAsync("NotionClientSecret");
    var functionUrl = await SecretsManager.GetSecretAsync("FunctionUrl");
    var redirectUrl = HttpUtility.HtmlEncode($"{functionUrl.Value}/api/token");
    var tokenUri = new Uri($"{notionOAuthUrl}/token");

    var notionAuthSecret = Encoding.UTF8.GetBytes($"{notionClientId.Value}{notionClientSecret.Value}");
    var notionBase64Auth = Convert.ToBase64String(notionAuthSecret);

    var request = new HttpRequestMessage(HttpMethod.Post, tokenUri)
    {
      Content =
        new FormUrlEncodedContent(new[]
        {
          new KeyValuePair<string, string>("grant_type", "authorization_code"),
          new KeyValuePair<string, string>("code", code!),
          new KeyValuePair<string, string>("client_id", notionClientId.Value),
          new KeyValuePair<string, string>("redirect_uri", redirectUrl),
        }),
    };

    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
    request.Content.Headers.Add("Notion-Version", "2022-06-28");
    request.Content.Headers.Add("Authorization", $"Basic {notionBase64Auth}");

    Debug.WriteLine($"Request: {request.RequestUri}");
    Debug.WriteLine($"Content: {await request.Content.ReadAsStringAsync()}");

    using var client = new HttpClient();
    try
    {
      var responseMessage = await client.SendAsync(request);
      responseMessage.EnsureSuccessStatusCode();

      var responseContent = await responseMessage.Content.ReadAsStringAsync();
      var responseJson = JsonDocument.Parse(responseContent);

      var accessToken = responseJson.RootElement.GetProperty("access_token").GetString();
      var botId = responseJson.RootElement.GetProperty("bot_id").GetString();

      Debug.WriteLine($"Access Token: {accessToken}");
      Debug.WriteLine($"Bot Id: {botId}");

      var responseUrl = $"cmdpalnotionext://oauth_redirect_uri/?access_token={accessToken}&bot_id={botId}";
      return new ContentResult() { Content = responseUrl };
    }
    catch (HttpRequestException ex)
    {
      Debug.WriteLine($"Request failed: {ex.Message}");
    }
    catch (JsonException ex)
    {
      Debug.WriteLine($"JSON parsing failed: {ex.Message}");
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Unexpected error: {ex.Message}");
    }

    return errorResult;
  }
}