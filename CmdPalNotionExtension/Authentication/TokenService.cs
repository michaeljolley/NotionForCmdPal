using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace CmdPalNotionExtension.Authentication;

internal sealed class TokenService
{
  private static readonly Lock _oAuthRequestsLock = new();
  private readonly ICredentialVault _credentialVault;

  private readonly OAuthClient _oAuthClient;

  public event EventHandler<bool>? SignInStateChanged;

  public TokenService(ICredentialVault credentialVault, OAuthClient oAuthClient)
  {
    _credentialVault = credentialVault;
    _oAuthClient = oAuthClient;
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

    lock (_oAuthRequestsLock)
    {
      try
      {
        _oAuthClient.BeginOAuthRequest();
        return _oAuthClient;
      }
      catch (Exception ex)
      {
        _log.Error(ex, $"Unable to complete OAuth request: ");
      }
      _oAuthClient.AccessTokenChanged += OAuthTokenEventHandler;
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