using System;

namespace CmdPalNotionExtension.Exceptions;

internal abstract class ExtensionException : Exception
{
  public ExtensionException() { }

  public ExtensionException(string message) : base(message) { }

  public ExtensionException(string message, System.Exception inner) : base(message, inner) { }
}