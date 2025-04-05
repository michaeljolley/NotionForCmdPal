using System;
using System.Net;

namespace NotionExtension.Authentication;

public sealed class TokenService
{
  private readonly ICredentialVault _credentialVault;

  private readonly OAuthClient _oAuthClient;

  public event EventHandler<bool>? LoginStateChanged;

  public TokenService(ICredentialVault credentialVault, OAuthClient oAuthClient)
  {
    _credentialVault = credentialVault;
    _oAuthClient = oAuthClient;
  }

  public bool IsLoggedIn()
  {
    var accessToken = _credentialVault.GetCredentials("NotionToken")?.Password;
    return !string.IsNullOrEmpty(accessToken);
  }

  public string GetAccessToken()
  {
    var accessToken = _credentialVault.GetCredentials("NotionToken")?.Password;
    return accessToken!;
  }

  public void SaveOrOverwriteAccessToken(string accessToken)
  {
    _credentialVault.SaveCredentials("NotionToken", new NetworkCredential(string.Empty, accessToken).SecurePassword);
  }

  public void StartLoginUser()
  {
    _oAuthClient.AccessTokenChanged += OAuthTokenEventHandler;
    OAuthClient.BeginOAuthRequest();
  }

  public void LogoutUser()
  {
    _credentialVault.RemoveAllCredentials();
    LoginStateChanged?.Invoke(this, false);
  }

  public void OAuthTokenEventHandler(object? sender, OAuthEventArgs e)
  {
    if (string.IsNullOrEmpty(e.AccessToken))
    {
      throw new Exception("No credentials found.");
    }

    SaveOrOverwriteAccessToken(e.AccessToken!);
    LoginStateChanged?.Invoke(this, true);
  }
}