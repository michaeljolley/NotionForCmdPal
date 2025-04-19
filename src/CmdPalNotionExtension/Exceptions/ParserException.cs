namespace CmdPalNotionExtension.Exceptions;

internal sealed class ParserException : ExtensionException
{
  public ParserException() : base() { }
  public ParserException(string message) : base(message) { }
  public ParserException(string message, System.Exception inner) : base(message, inner) { }
}
