using System;
using System.IO;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CmdPalNotionExtension.Helpers;

internal sealed class NotionHelper
{
  internal static IconInfo Icon => IconHelpers.FromRelativePath("Assets\\Square88x88Logo.png");

  public static string LogoWithBackplatePath { get; } = Path.Combine(AppContext.BaseDirectory, "Assets", "Square300x300Logo.png");

  internal const string BaseExtensionId = "com.baldbeardedbuilder.cmdpal.notion";
  
  internal static string StateJsonPath()
  {
    var directory = Utilities.BaseSettingsPath("Microsoft.CmdPal");
    Directory.CreateDirectory(directory);

    return Path.Combine(directory, $"{BaseExtensionId}.state.json");
  }
  public static string GetBase64Icon(string iconPath)
  {
    if (!string.IsNullOrEmpty(iconPath))
    {
      var bytes = File.ReadAllBytes(iconPath);
      return Convert.ToBase64String(bytes);
    }

    return string.Empty;
  }
}