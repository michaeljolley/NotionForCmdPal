using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Authentication;

namespace CmdPalNotionExtension.Commands;

internal sealed partial class SignOutCommand : InvokableCommand
{
  private readonly TokenService _tokenService;

  internal SignOutCommand(TokenService tokenService)
  {
    _tokenService = tokenService;
    Name = "Sign out of Notion";
    Icon = new IconInfo("\uF3B1");
  }

  public override CommandResult Invoke()
  {
    _tokenService.SignOutUser();
    return CommandResult.KeepOpen();
  }

  public ICommandItem ToCommandItem()
  {
    return new CommandItem(this)
    {
      Title = this.Name,
      Subtitle = "Sign out of Notion",
    };
  }
}