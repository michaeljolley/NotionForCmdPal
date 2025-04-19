using CmdPalNotionExtension.Helpers;
using CmdPalNotionExtension.ListItems;
using CmdPalNotionExtension.Notion;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Collections.Generic;
using System.Linq;

namespace CmdPalNotionExtension.Controls.Pages;

internal sealed partial class RecentPagesPage : ListPage
{
  private readonly NotionDataProvider _dataProvider;
  private readonly ListItemFactory _listItemFactory;
  private readonly Resources _resources;

  private string? _cursor = string.Empty;
  private List<IListItem> _currentPages = new List<IListItem>();
  
  public RecentPagesPage(
    NotionDataProvider dataProvider, 
    ListItemFactory listItemFactory,
    Resources resources)
  {
    _dataProvider = dataProvider;
    _listItemFactory = listItemFactory;
    _resources = resources;

    Title = _resources.GetResource("Pages_Recent_Pages_Title");
    Icon = new("\uE823");
  }

  public override IListItem[] GetItems()
  {
    var res = _dataProvider.GetRecentNotionPagesAsync(_cursor).GetAwaiter().GetResult();

    if (res != null)
    {
      _cursor = res.NextCursor;
      _currentPages.AddRange(res.Results.Select(s => _listItemFactory.Create(s)));
    }

    return _currentPages.ToArray();
  }

  public CommandItem ToCommandItem()
  {
    return new CommandItem(this)
    {
      Title = _resources.GetResource("Pages_Recent_Pages_Title"),
      Subtitle = _resources.GetResource("Pages_Recent_Pages_SubTitle"),
      Icon = NotionHelper.Icon
    };
  }
}