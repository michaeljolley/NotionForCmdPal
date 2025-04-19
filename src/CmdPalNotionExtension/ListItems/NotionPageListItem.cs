using CmdPalNotionExtension.Notion.Models;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Linq;
using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.ListItems;

internal sealed partial class NotionPageListItem : ListItem
{
  private const int _maxNumberOfTags = 4;

  public NotionPageListItem(NotionPage notionPage, ICommand command) : base(command)
  {
    var titleProp = notionPage.Properties?["Name"];
    var title = "Unknown page";

    if (titleProp != null)
    {
      title = string.Join(" ", ((TitleProperty)titleProp).TitleDetails.SelectMany(s => s.PlainText).ToArray());
    }


    Title = title;
    //Tags = anime.Genres.Select(genre => new Tag
    //{
    //  Text = genre,
    //}).Take(_maxNumberOfTags).ToArray();
  }
}