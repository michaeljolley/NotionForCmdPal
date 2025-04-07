using System;

namespace CmdPalNotionExtension.Authentication;

public class SignInStatusChangedEventArgs : EventArgs
{
  public bool IsSignedIn { get; }

  public Exception? Error { get; }

  public SignInStatusChangedEventArgs(bool isSignedIn, Exception? error = null)
  {
    IsSignedIn = isSignedIn;
    Error = error;
  }
}