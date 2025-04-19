using CmdPalNotionExtension.Notion.Models;

namespace CmdPalNotionExtension.Exceptions;

internal sealed class NotionAPIErrorException : ExtensionException
{
  public Error Payload;

  public NotionAPIErrorException(Error payload) : base()
  {
    Payload = payload;
  }
}
