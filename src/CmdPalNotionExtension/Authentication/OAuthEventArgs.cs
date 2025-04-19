using System;

namespace CmdPalNotionExtension.Authentication;

public sealed class OAuthEventArgs : EventArgs
{
  public string? AccessToken { get; }

  public OAuthEventArgs(string? token)
  {
    AccessToken = token;
  }
}