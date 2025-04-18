using System;
using System.Collections.Generic;

using CmdPalNotionExtension.Helpers;

namespace CmdPalNotionExtension.Controls.Forms;

public interface INotionForm
{
  event EventHandler<bool>? LoadingStateChanged;

  event EventHandler<FormSubmitEventArgs>? FormSubmitted;

  Dictionary<string, string> TemplateSubstitutions { get; }
}