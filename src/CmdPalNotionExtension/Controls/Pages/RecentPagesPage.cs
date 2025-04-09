using System.Linq;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

using CmdPalNotionExtension.Notion;
using CmdPalNotionExtension.ListItems;

namespace CmdPalNotionExtension.Controls.Pages;

internal sealed partial class RecentPagesPage : ListPage
{
  private readonly NotionDataProvider _dataProvider;
  private readonly ListItemFactory _listItemFactory;
  
  public RecentPagesPage(NotionDataProvider dataProvider, ListItemFactory listItemFactory)
  {
    _dataProvider = dataProvider;
    _listItemFactory = listItemFactory;

    Title = "Recent Pages";
    Icon = new("\uE823");
  }

  public override IListItem[] GetItems()
  {
    var res = _dataProvider.GetRecentNotionPagesAsync().GetAwaiter().GetResult();

    return res.Select(item => _listItemFactory.Create(item)).ToArray();
  }

  public CommandItem ToCommandItem()
  {
    return new CommandItem(this)
    {
      Title = "Recent Pages",
      Subtitle = "Recently viewed pages",
    };
  }
}