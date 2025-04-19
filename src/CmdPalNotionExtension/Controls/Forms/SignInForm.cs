using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

using CmdPalNotionExtension.Authentication;
using CmdPalNotionExtension.Helpers;

namespace CmdPalNotionExtension.Controls.Forms;

internal partial class SignInForm : FormContent, INotionForm
{
  public event EventHandler<bool>? LoadingStateChanged;

  public event EventHandler<FormSubmitEventArgs>? FormSubmitted;

  private readonly TokenService _tokenService;
  private readonly IResources _resources;

  private bool _isButtonEnabled = true;

  private string IsButtonEnabled =>
      _isButtonEnabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture);

  public SignInForm(TokenService tokenService, IResources resources)
  {
    _resources = resources;
    _tokenService = tokenService;
    _tokenService.SignInStateChanged += SignInStateChanged;
  }

  private void SignOutForm_SignOutAction(object? sender, SignInStatusChangedEventArgs e)
  {
    _isButtonEnabled = !e.IsSignedIn;
  }

  private void SignInStateChanged(object? sender, bool isSignedIn)
  {
    SetButtonEnabled(!isSignedIn);
    LoadingStateChanged?.Invoke(this, false);
    FormSubmitted?.Invoke(this, new FormSubmitEventArgs(false, null));
  }

  private void SetButtonEnabled(bool isEnabled)
  {
    _isButtonEnabled = isEnabled;
    TemplateJson = TemplateHelper.LoadTemplateJsonFromTemplateName("AuthTemplate", TemplateSubstitutions);
    OnPropertyChanged(nameof(TemplateJson));
  }

  public Dictionary<string, string> TemplateSubstitutions => new()
    {
        { "{{AuthTitle}}", _resources.GetResource("Forms_Sign_In") },
        { "{{AuthButtonTitle}}", _resources.GetResource("Forms_Sign_In") },
        { "{{AuthIcon}}", $"data:image/png;base64,{NotionHelper.GetBase64Icon(NotionHelper.LogoWithBackplatePath)}" },
        { "{{AuthButtonTooltip}}", _resources.GetResource("Forms_Sign_In_Tooltip") },
        { "{{ButtonIsEnabled}}", IsButtonEnabled },
    };

  public override string TemplateJson => TemplateHelper.LoadTemplateJsonFromTemplateName("AuthTemplate", TemplateSubstitutions);

  public override ICommandResult SubmitForm(string inputs, string data)
  {
    LoadingStateChanged?.Invoke(this, true);
    Task.Run(() =>
    {
      try
      {
        LoadingStateChanged?.Invoke(this, false);
        _tokenService.StartSignInUser();
        SetButtonEnabled(false);
        FormSubmitted?.Invoke(this, new FormSubmitEventArgs(true, null));
      }
      catch (Exception ex)
      {
        LoadingStateChanged?.Invoke(this, false);
        SetButtonEnabled(true);
        FormSubmitted?.Invoke(this, new FormSubmitEventArgs(false, ex));
      }
    });
    // Dismiss here so the user will see other available commands once logged in.
    return CommandResult.Dismiss();
  }
}