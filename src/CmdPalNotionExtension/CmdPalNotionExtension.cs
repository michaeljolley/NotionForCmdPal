using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CmdPalNotionExtension;

[ComVisible(true)]
[Guid("d04193f6-842f-4af4-924a-58a0cc7227f4")]
[ComDefaultInterface(typeof(IExtension))]
public sealed partial class CmdPalNotionExtension : IExtension, IDisposable
{
  private readonly ManualResetEvent _extensionDisposedEvent;

  private readonly CommandProvider _provider;

  public CmdPalNotionExtension(ManualResetEvent extensionDisposedEvent, CommandProvider provider)
  {
    this._extensionDisposedEvent = extensionDisposedEvent;
    this._provider = provider;
  }

  public object? GetProvider(ProviderType providerType)
  {
    return providerType switch
    {
      ProviderType.Commands => _provider,
      _ => null,
    };
  }

  public void Dispose() => this._extensionDisposedEvent.Set();
}
