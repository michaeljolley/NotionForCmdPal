using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Authentication;
using CmdPalNotionExtension.Controls.Pages;
using CmdPalNotionExtension.Helpers;

namespace CmdPalNotionExtension;

internal partial class CmdPalNotionExtensionCommandsProvider : CommandProvider
{
  private readonly TokenService _tokenService;
  private readonly SignInPage _signInPage;
  private readonly SignOutPage _signOutPage;
  private readonly RecentPage _recentPagesPage;
  private readonly Resources _resources;
  private bool _isSignedIn;

  public CmdPalNotionExtensionCommandsProvider(
        TokenService tokenService,
        RecentPage recentPagesPage,
        Resources resources,
        SignInPage signInPage,
        SignOutPage signOutPage)
  {
    _resources = resources;
    _tokenService = tokenService;
    _signInPage = signInPage;
    _signOutPage = signOutPage;
    _recentPagesPage = recentPagesPage;

    DisplayName = _resources.GetResource("ExtensionTitle");
    Icon = NotionHelper.Icon;
    Id = NotionHelper.BaseExtensionId;

    _tokenService.SignInStateChanged += OnSignInStatusChanged;

    // This async method raises the RaiseItemsChanged event to update the top-level commands
    // So it is safe if we let it run asynchronously as "fire and forget"
    _ = UpdateSignInStatus(_tokenService.IsSignedIn());
  }

  private void UpdateTopLevelCommands() => RaiseItemsChanged(0);

  private void OnSignInStatusChanged(object? sender, bool isSignedIn)
  {
    _ = UpdateSignInStatus(isSignedIn);
  }

  public async Task UpdateSignInStatus(bool isSignedIn)
  {
    _isSignedIn = isSignedIn;
    UpdateTopLevelCommands();
  }

  public override ICommandItem[] TopLevelCommands()
  {
    if (!_isSignedIn)
    {
      return new[]
      {
        new CommandItem(_signInPage)
        {
          Title = _resources.GetResource("ExtensionTitle"),
          Subtitle = _resources.GetResource("Forms_Sign_In"),
          Icon = NotionHelper.Icon
        },
      };
    }

    var commands = new List<CommandItem>
    {
      new(_signOutPage)
      {
        Title = _resources.GetResource("ExtensionTitle"),
        Subtitle = _resources.GetResource("Forms_Sign_Out_Button_Title"),
        Icon = NotionHelper.Icon
      },
      _recentPagesPage.ToCommandItem()
    };

    return commands.ToArray();
  }
}