using System.Diagnostics;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc;

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
    var errorResult = new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = "There was a problem authenticating with Notion. Please try again later." };

    var queryString = response.Query;
    var queryStringCollection = HttpUtility.ParseQueryString(queryString);

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
    var tokenUri = new Uri($"{notionOAuthUrl.Value}/token");

    var notionAuthSecret = Encoding.UTF8.GetBytes($"{notionClientId.Value}:{notionClientSecret.Value}");
    var notionBase64Auth = Convert.ToBase64String(notionAuthSecret);

    var oauthToken = new OAuthToken(code!, redirectUrl);

    var request = new HttpRequestMessage(HttpMethod.Post, tokenUri)
    {
      Content = new StringContent(JsonSerializer.Serialize(oauthToken), Encoding.UTF8, "application/json")
    };

    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    request.Content.Headers.Add("Notion-Version", "2022-06-28");
    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", notionBase64Auth);

    using var client = new HttpClient();
    try
    {
      var responseMessage = await client.SendAsync(request);
      responseMessage.EnsureSuccessStatusCode();

      var responseContent = await responseMessage.Content.ReadFromJsonAsync<OAuthResponse>();

      if (!responseContent!.IsSuccess) 
      {
        return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"There was a problem authenticating with Notion. Please try again later.\n\n{responseContent.Error}\n{responseContent.ErrorDescription}" };
      }

      Debug.WriteLine($"Access Token: {responseContent.AccessToken}");
      Debug.WriteLine($"Bot Id: {responseContent.BotId}");

      var responseUrl = $"cmdpalnotionext://oauth_redirect_uri/?access_token={responseContent.AccessToken}&bot_id={responseContent.BotId}";
      return new RedirectResult(responseUrl);
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