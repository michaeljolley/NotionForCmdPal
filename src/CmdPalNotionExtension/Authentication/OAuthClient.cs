using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace NotionExtension.Authentication;

public sealed class OAuthClient
{
  public event EventHandler<OAuthEventArgs>? AccessTokenChanged;

  private const string apiAuthUrl = "https://notionforcmdpaloauthapi-abctftdfe2cacufe.southcentralus-01.azurewebsites.net/api/authorize";

  private static Uri CreateOAuthRequestUri()
  {
    return new Uri(apiAuthUrl);
  }

  public static void BeginOAuthRequest()
  {
    var uri = CreateOAuthRequestUri();
    var options = new Windows.System.LauncherOptions();
    var browserLaunch = false;

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

  public void HandleOAuthRedirection(Uri response)
  {
    var queryString = response.Query;
    var queryStringCollection = HttpUtility.ParseQueryString(queryString);

    if (queryStringCollection["error"] != null)
    {
      // Handle error
      Debug.WriteLine($"Error: {queryStringCollection["error"]}");
    }

    if (queryStringCollection["access_token"] == null)
    {
      Debug.WriteLine("No token found in the response.");
    }

    // Handle success
    var accessToken = queryStringCollection["access_token"];
    var botId = queryStringCollection["bot_id"];
    Debug.WriteLine($"Access Token: {accessToken}");

    AccessTokenChanged?.Invoke(null, new OAuthEventArgs(accessToken, botId));
  }
}