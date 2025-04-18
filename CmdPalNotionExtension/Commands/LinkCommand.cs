using System.Diagnostics;
using Microsoft.CommandPalette.Extensions.Toolkit;

using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.Commands;  
internal sealed partial class LinkCommand : InvokableCommand
{
  private readonly string _htmlUrl;

  internal LinkCommand(NotionPage notionPage)
  {
    _htmlUrl = notionPage.Url;
    Name = "Open in browser";
    Icon = new IconInfo("\uE8A7");
  }

  public override CommandResult Invoke()
  {
    Process.Start(new ProcessStartInfo(_htmlUrl) { UseShellExecute = true });
    return CommandResult.KeepOpen();
  }
}