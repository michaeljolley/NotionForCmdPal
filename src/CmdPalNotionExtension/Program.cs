using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.Windows.AppLifecycle;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Activation;

using Shmuelie.WinRTServer;
using Shmuelie.WinRTServer.CsWinRT;

using CmdPalNotionExtension.Authentication;
using CmdPalNotionExtension.Commands;
using CmdPalNotionExtension.Controls.Forms;
using CmdPalNotionExtension.Controls.Pages;
using CmdPalNotionExtension.Notion;
using CmdPalNotionExtension.ListItems;
using CmdPalNotionExtension.Helpers;

namespace CmdPalNotionExtension;

public class Program
{
  private static OAuthClient? _oAuthClient;

  [MTAThread]
  public static async Task Main(string[] args)
  {
    // Force the app to be single instanced.
    // Get or register the main instance.
    var mainInstance = AppInstance.FindOrRegisterForKey("mainInstance");
    var activationArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
    if (!mainInstance.IsCurrent)
    {
      await mainInstance.RedirectActivationToAsync(activationArgs);
      return;
    }

    // Register for activation redirection.
    AppInstance.GetCurrent().Activated += AppActivationRedirected;

    if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
    {
      try
      {
        await HandleCOMServerActivationAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error: {ex.Message}");
      }
    }
    else
    {
      Console.WriteLine("Not being launched as a Extension... exiting.");
    }
  }

  private static async void AppActivationRedirected(object? sender, AppActivationArguments activationArgs)
  {
    // Handle COM server.
    if (activationArgs.Kind == ExtendedActivationKind.Launch)
    {
      var d = activationArgs.Data as ILaunchActivatedEventArgs;
      var args = d?.Arguments.Split();

      if (args?.Length > 1 && args[1] == "-RegisterProcessAsComServer")
      {
        await HandleCOMServerActivationAsync();
      }
    }

    // Handle Protocol.
    if (activationArgs.Kind == ExtendedActivationKind.Protocol)
    {
      var d = activationArgs.Data as IProtocolActivatedEventArgs;
      if (d is not null)
      {
        HandleProtocolActivation(d.Uri);
      }
    }
  }

  private static async Task HandleCOMServerActivationAsync()
  {
    await using global::Shmuelie.WinRTServer.ComServer server = new();
    var extensionDisposedEvent = new ManualResetEvent(false);

    var credentialVault = new CredentialVault();
    var oAuthClient = new OAuthClient();
    _oAuthClient = oAuthClient;

    var tokenService = new TokenService(credentialVault, oAuthClient);

    var path = ResourceLoader.GetDefaultResourceFilePath();
    var resourceLoader = new ResourceLoader(path);
    var resources = new Resources(resourceLoader);

    var dataProvider = new NotionDataProvider(tokenService);

    var signInForm = new SignInForm(tokenService, resources);
    var signOutForm = new SignOutForm(tokenService, resources);
    var signInPage = new SignInPage(signInForm, new StatusMessage(), resources.GetResource("Message_Sign_In_Success"), resources.GetResource("Message_Sign_In_Fail"));
    var signOutPage = new SignOutPage(signOutForm, new StatusMessage(), resources.GetResource("Message_Sign_Out_Success"), resources.GetResource("Message_Sign_Out_Fail"));

    var commandFactory = new CommandFactory(dataProvider);
    var listItemFactory = new ListItemFactory(commandFactory, tokenService, dataProvider);

    var recentPagesPage = new RecentPagesPage(dataProvider, listItemFactory);

    var commandProvider = new CmdPalNotionExtensionCommandsProvider(
        tokenService, recentPagesPage, resources, signInPage, signOutPage);

    var extensionInstance = new CmdPalNotionExtension(extensionDisposedEvent, commandProvider);

    // We are instantiating an extension instance once above, and returning it every time the callback in RegisterExtension below is called.
    // This makes sure that only one instance of CmdPalNotionExtension is alive, which is returned every time the host asks for the IExtension object.
    // If you want to instantiate a new instance each time the host asks, create the new instance inside the delegate.
    server.RegisterClass<CmdPalNotionExtension, IExtension>(() => extensionInstance);
    server.Start();

    // This will make the main thread wait until the event is signaled by the extension class.
    // Since we have single instance of the extension object, we exit as soon as it is disposed.
    extensionDisposedEvent.WaitOne();
  }

  private static void HandleProtocolActivation(Uri oauthRedirectUri) => _oAuthClient?.HandleOAuthRedirection(oauthRedirectUri);
}
