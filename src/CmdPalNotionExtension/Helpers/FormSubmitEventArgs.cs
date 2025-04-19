using System;

namespace CmdPalNotionExtension.Helpers;

public class FormSubmitEventArgs : EventArgs
{
  // Status can mean success or failure as well as signed in or signed out
  public bool Status { get; }

  public Exception? Exception { get; }

  public FormSubmitEventArgs(bool status, Exception? ex)
  {
    Status = status;
    Exception = ex;
  }
}