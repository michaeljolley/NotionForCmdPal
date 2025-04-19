using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Http;
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
    var queryString = response.Query;
    var queryStringCollection = HttpUtility.ParseQueryString(queryString);

    if (queryStringCollection["error"] != null)
    {
      // Handle error
      Debug.WriteLine($"Error: {queryStringCollection["error"]}");
      return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"There was a problem authenticating with Notion. Please try again later. {queryStringCollection["error"]}" };
    }

    if (queryStringCollection["code"] == null)
    {
      Debug.WriteLine("No code found in the response.");
      return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"There was a problem authenticating with Notion. Please try again later. No code found in the response." };
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
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", notionBase64Auth);

    using var client = new HttpClient();
    try
    {
      var responseMessage = await client.SendAsync(request);
      responseMessage.EnsureSuccessStatusCode();

      var responseJson = await responseMessage.Content.ReadAsStringAsync();
      var responseContent = JsonSerializer.Deserialize<OAuthResponse>(responseJson);

      if (!responseContent!.IsSuccess)
      {
        return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"There was a problem authenticating with Notion. Please try again later.\n\n{responseMessage.StatusCode}\n{responseContent.Error}\n{responseContent.ErrorDescription}" };
      }

      var responseUrl = $"cmdpalnotionext://oauth_redirect_uri/?access_token={responseContent.AccessToken}&bot_id={responseContent.BotId}";

      return new ContentResult()
      {
        Content = String.Format(successMessage, responseUrl),
        ContentType = "text/html",
        StatusCode = 200
      };
    }
    catch (HttpRequestException ex)
    {
      Debug.WriteLine($"Request failed: {ex.Message}");
      return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"Request failed: There was a problem authenticating with Notion. Please try again later. }\n\n{ex.Message}" };
    }
    catch (JsonException ex)
    {
      Debug.WriteLine($"JSON parsing failed: {ex.Message}");
      return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"JSON parsing failed: There was a problem authenticating with Notion. Please try again later. \n\n{ex.Message}" };
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Unexpected error: {ex.Message}");
      return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"Unexpected error: There was a problem authenticating with Notion. Please try again later. \n\n{ex.Message}" };
    }

    return new ContentResult() { StatusCode = 500, ContentType = "text/plain", Content = $"There was a problem authenticating with Notion. Please try again later." };
  }

  private static string successMessage = @"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Successfully Signed in to Notion</title>
        <style>
            html, body {
                display: flex;
                flex-direction: column;
                gap: 2rem;
                justify-content: center;
                align-items: center;
                height: 100vh;
                margin: 0;
                font-family: Arial, Helvetica, sans-serif;
            }
            h1 {
                font-size: 2rem;
                color: #222;
            }
            p {
                font-size: 1.2rem;
                color: #555;
            }
        </style>
    </head>
    <body>
        <h1>You are now logged in!</h1>
        <p>You can now close this window and reopen Command Palette to access your Notion account.</p>
        <script type='text/javascript'>
            window.location.href = '{0}';
        </script>
    </body>
    </html>";
}