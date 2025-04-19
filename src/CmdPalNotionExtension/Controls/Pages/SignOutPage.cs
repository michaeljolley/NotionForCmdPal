using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Helpers;
using CmdPalNotionExtension.Controls.Forms;

namespace CmdPalNotionExtension.Controls.Pages;

internal sealed partial class SignOutPage : ContentPage
{
  private readonly SignOutForm _signOutForm;
  private readonly StatusMessage _statusMessage;
  private readonly string _successMessage;
  private readonly string _errorMessage;

  public SignOutPage(SignOutForm signOutForm, StatusMessage statusMessage, string successMessage, string errorMessage)
  {
    _signOutForm = signOutForm;
    _statusMessage = statusMessage;
    _successMessage = successMessage;
    _errorMessage = errorMessage;

    // Wire up events using the helper
    FormEventHelper.WireFormEvents(_signOutForm, this, _statusMessage, _successMessage, _errorMessage);

    // Hide status message initially
    ExtensionHost.HideStatus(_statusMessage);
  }

  public override IContent[] GetContent()
  {
    ExtensionHost.HideStatus(_statusMessage);
    return [_signOutForm];
  }
}