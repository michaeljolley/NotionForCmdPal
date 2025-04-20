using System.Collections.Generic;
using System.Linq;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Helpers;
using CmdPalNotionExtension.ListItems;
using CmdPalNotionExtension.Notion;

namespace CmdPalNotionExtension.Controls.Pages;

internal sealed partial class RecentPage : DynamicListPage
{
  private readonly NotionDataProvider _dataProvider;
  private readonly ListItemFactory _listItemFactory;
  private readonly Resources _resources;

  private string? _cursor = string.Empty;
  private bool _hasMore;
  private List<IListItem> _currentPages = new List<IListItem>();
  
  public RecentPage(
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
    if (_cursor == string.Empty)
    {
      var res = GetSearchResult();
      if (res != null)
      {
        _cursor = res.NextCursor;
        _hasMore = res.HasMore;
        _currentPages.AddRange(res.Results.Select(s => _listItemFactory.Create(s)));
      }
    }

    return _currentPages.ToArray();
  }

  public override void UpdateSearchText(string oldSearch, string newSearch)
  {
    _currentPages = new List<IListItem>();
    _hasMore = false;
    _cursor = string.Empty;
    RaiseItemsChanged();
  }

  public override void LoadMore()
  {
    if (_hasMore)
    {
      var res = GetSearchResult();
      if (res != null)
      {
        _cursor = res.NextCursor;
        _hasMore = res.HasMore;
        _currentPages.AddRange(res.Results.Select(s => _listItemFactory.Create(s)));
      }
      RaiseItemsChanged();

      base.LoadMore();
    }
  }

  private SearchResult GetSearchResult()
  {
    var res = _dataProvider.GetRecentNotionPagesAsync(SearchText, _cursor).GetAwaiter().GetResult();
    return res;
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