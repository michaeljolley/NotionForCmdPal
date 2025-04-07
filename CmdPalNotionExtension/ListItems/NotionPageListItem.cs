using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.ListItems;

internal sealed partial class NotionPageListItem : ListItem
{
  private const int _maxNumberOfTags = 4;

  public NotionPageListItem(NotionPage notionPage, ICommand command) : base(command)
  {
    Title = notionPage.Properties["Name"].title.plain_text;
    //Tags = anime.Genres.Select(genre => new Tag
    //{
    //  Text = genre,
    //}).Take(_maxNumberOfTags).ToArray();
  }
}