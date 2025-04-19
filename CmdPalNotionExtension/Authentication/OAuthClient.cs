using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace CmdPalNotionExtension.Authentication;

internal sealed class OAuthClient
{
  public event EventHandler<OAuthEventArgs>? AccessTokenChanged;
  private const string apiAuthUrl = "https://notionforcmdpaloauthapi-abctftdfe2cacufe.southcentralus-01.azurewebsites.net/api/authorize";

  internal DateTime StartTime
  {
    get; private set;
  }

  private static Uri CreateOauthRequestUri()
  {
    return new Uri(apiAuthUrl);
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

  public void HandleOAuthRedirection(Uri response)
  {
    var queryString = response.Query;
    var queryStringCollection = HttpUtility.ParseQueryString(queryString);

    if (queryStringCollection["error"] != null)
    {
      // Handle error
      throw new InvalidOperationException($"Error: {queryStringCollection["error"]}");
    }

    if (queryStringCollection["access_token"] == null)
    {
      throw new InvalidOperationException("No token found in the response.");
    }

    // Handle success
    var accessToken = queryStringCollection["access_token"];
    var botId = queryStringCollection["bot_id"];

    AccessTokenChanged?.Invoke(null, new OAuthEventArgs(accessToken, botId));
  }
}