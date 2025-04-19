using System;
using System.Net;

namespace CmdPalNotionExtension.Authentication;

internal sealed class TokenService
{
  private readonly ICredentialVault _credentialVault;

  private readonly OAuthClient _oAuthClient;

  public event EventHandler<bool>? SignInStateChanged;

  public TokenService(ICredentialVault credentialVault, OAuthClient oAuthClient)
  {
    _credentialVault = credentialVault;
    _oAuthClient = oAuthClient;
    _oAuthClient.AccessTokenChanged += OAuthTokenEventHandler;
  }

  public bool IsSignedIn()
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

  public void StartSignInUser()
  {
    _oAuthClient.BeginOAuthRequest();
  }

  public void SignOutUser()
  {
    _credentialVault.RemoveAllCredentials();
    SignInStateChanged?.Invoke(this, false);
  }

  public void OAuthTokenEventHandler(object? sender, OAuthEventArgs e)
  {
    if (string.IsNullOrEmpty(e.AccessToken))
    {
      throw new Exception("No credentials found.");
    }

    SaveOrOverwriteAccessToken(e.AccessToken!);
    SignInStateChanged?.Invoke(this, true);
  }
}