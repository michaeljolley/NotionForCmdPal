using System;

namespace NotionExtension.Authentication;

public sealed class OAuthEventArgs : EventArgs
{
  public string? AccessToken { get; }

  public string? BotId { get; }

  public OAuthEventArgs(string? token, string? botId)
  {
    AccessToken = token;
    BotId = botId;
  }
}