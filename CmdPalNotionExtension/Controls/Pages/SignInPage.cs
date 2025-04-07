using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Helpers;
using CmdPalNotionExtension.Controls.Forms;

namespace CmdPalNotionExtension.Controls.Pages;

internal sealed partial class SignInPage : ContentPage
{
  private readonly SignInForm _signInForm;
  private readonly StatusMessage _statusMessage;
  private readonly string _successMessage;
  private readonly string _errorMessage;

  public SignInPage(SignInForm signInForm, StatusMessage statusMessage, string successMessage, string errorMessage)
  {
    Name = "Sign In to Notion";
    Icon = IconHelpers.FromRelativePath("Assets\\Square88x88Logo.png");
    Id = $"{NotionHelper.BaseExtensionId}.signinpage";

    _signInForm = signInForm;
    _statusMessage = statusMessage;
    _successMessage = successMessage;
    _errorMessage = errorMessage;

    // Wire up events using the helper
    FormEventHelper.WireFormEvents(_signInForm, this, _statusMessage, _successMessage, _errorMessage);

    _signInForm.PropChanged += UpdatePage;

    // Hide status message initially
    ExtensionHost.HideStatus(_statusMessage);
  }

  private void UpdatePage(object sender, IPropChangedEventArgs args)
  {
    RaiseItemsChanged();
  }

  public override IContent[] GetContent()
  {
    ExtensionHost.HideStatus(_statusMessage);
    return [_signInForm];
  }
}