using CmdPalNotionExtension.Notion;

namespace CmdPalNotionExtension.Commands;

internal sealed class CommandFactory
{
  private readonly NotionDataProvider _dataProvider;

  public CommandFactory(NotionDataProvider dataProvider)
  {
    _dataProvider = dataProvider;
  }
}