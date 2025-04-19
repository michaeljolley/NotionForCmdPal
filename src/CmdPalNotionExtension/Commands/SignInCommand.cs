using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Authentication;

namespace CmdPalNotionExtension.Commands;

internal sealed partial class SignInCommand : InvokableCommand
{
  private readonly TokenService _tokenService;

  internal SignInCommand(TokenService tokenService)
  {
    Name = "Connect to Notion";
    Icon = new("\uE961");
    _tokenService = tokenService;
  }

  public override CommandResult Invoke()
  {
    _tokenService.StartSignInUser();
    return CommandResult.KeepOpen();
  }

  internal ICommandItem ToCommandItem()
  {
    return new CommandItem(this)
    {
      Title = this.Name,
      Subtitle = "Sign in to Notion to access your data",
    };
  }
}
