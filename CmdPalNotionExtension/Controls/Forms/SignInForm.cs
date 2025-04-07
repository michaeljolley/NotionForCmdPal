using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

using CmdPalNotionExtension.Authentication;
using CmdPalNotionExtension.Helpers;

namespace CmdPalNotionExtension.Controls.Forms;

public partial class SignInForm : FormContent, INotionForm
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
    _tokenService.StartSignInUser();
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
        var signInSucceeded = HandleSignIn().Result;
        LoadingStateChanged?.Invoke(this, false);
        _authenticationMediator.SignIn(new SignInStatusChangedEventArgs(signInSucceeded, null));
        FormSubmitted?.Invoke(this, new FormSubmitEventArgs(signInSucceeded, null));
      }
      catch (Exception ex)
      {
        LoadingStateChanged?.Invoke(this, false);
        SetButtonEnabled(true);
        _authenticationMediator.SignIn(new SignInStatusChangedEventArgs(false, ex));
        FormSubmitted?.Invoke(this, new FormSubmitEventArgs(false, ex));
      }
    });
    return CommandResult.KeepOpen();
  }

  private async Task<bool> HandleSignIn()
  {
    var numPreviousDevIds = _tokenService.GetLoggedInDeveloperIdsInternal().Count();

    await _tokenService.LoginNewDeveloperIdAsync();

    var numDevIds = _tokenService.GetLoggedInDeveloperIdsInternal().Count();

    return numDevIds > numPreviousDevIds;
  }
}