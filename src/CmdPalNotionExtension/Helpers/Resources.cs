using System;
using Microsoft.Windows.ApplicationModel.Resources;

namespace CmdPalNotionExtension.Helpers;

public class Resources : IResources
{
  private readonly ResourceLoader _resourceLoader;

  public Resources(ResourceLoader resourceLoader)
  {
    _resourceLoader = resourceLoader;
  }

  public string GetResource(string identifier)
  {
    try
    {
      return _resourceLoader.GetString(identifier);
    }
    catch (Exception ex)
    {
      return identifier;
    }
  }
}

public interface IResources
{
  string GetResource(string identifier);
}