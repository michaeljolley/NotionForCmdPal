using System;
using System.Collections.Generic;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

using CmdPalNotionExtension.Authentication;
using CmdPalNotionExtension.Helpers;

namespace CmdPalNotionExtension.Controls.Forms;

public partial class SignOutForm : FormContent, INotionForm
{
  public event EventHandler<bool>? LoadingStateChanged;

  public event EventHandler<FormSubmitEventArgs>? FormSubmitted;

  private readonly TokenService _tokenService;
  private readonly IResources _resources;

  public SignOutForm(TokenService tokenService, IResources resources)
  {
    _tokenService = tokenService;
    _resources = resources;
  }

  public Dictionary<string, string> TemplateSubstitutions => new()
    {
        { "{{AuthTitle}}", _resources.GetResource("Forms_Sign_Out_Title") },
        { "{{AuthButtonTitle}}", _resources.GetResource("Forms_Sign_Out_Button_Title") },
        { "{{AuthIcon}}", $"data:image/png;base64,{NotionHelper.GetBase64Icon(NotionHelper.LogoWithBackplatePath)}" },
        { "{{AuthButtonTooltip}}", _resources.GetResource("Forms_Sign_Out_Tooltip") },
        { "{{ButtonIsEnabled}}", "true" },
    };

  public override string TemplateJson => TemplateHelper.LoadTemplateJsonFromTemplateName("AuthTemplate", TemplateSubstitutions);

  public override ICommandResult SubmitForm(string inputs, string data)
  {
    LoadingStateChanged?.Invoke(this, true);
    _tokenService.SignOutUser();
    LoadingStateChanged?.Invoke(this, false);
    FormSubmitted?.Invoke(this, new FormSubmitEventArgs(true, null));
    
    return CommandResult.KeepOpen();
  }
}