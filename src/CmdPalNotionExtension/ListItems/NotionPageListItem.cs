using System.Linq;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using CmdPalNotionExtension.Notion.Models;
using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.ListItems;

internal sealed partial class NotionPageListItem : ListItem
{
  private const int _maxNumberOfTags = 4;

  public NotionPageListItem(NotionPage notionPage, ICommand command) : base(command)
  {
    var title = "Unknown page";
    var icon = new IconInfo("\uE7C3");

    if (notionPage.Properties.ContainsKey("Name"))
    {
      var titleProp = notionPage.Properties["Name"];
      title = string.Join(" ", ((TitleProperty)titleProp).TitleDetails.SelectMany(s => s.PlainText).ToArray());
    } 
    else if (notionPage.Properties.ContainsKey("Title"))
    {
      var titleProp = notionPage.Properties["Title"];
      title = string.Join(" ", ((TitleProperty)titleProp).TitleDetails.SelectMany(s => s.PlainText).ToArray());
    }

    if (notionPage.Icon != null)
    {
      switch (notionPage.Icon.Type)
      {
        case "emoji":
          var emojiIcon = (EmojiIcon)notionPage.Icon;
          icon = new(emojiIcon.Emoji);
          break;
        case "file":
          var fileIcon = (FileIcon)notionPage.Icon;
          icon = new(fileIcon.File?.Url);
          break;
        case "custom_emoji":
          var customEmojiIcon = (CustomEmojiIcon)notionPage.Icon;
          icon = new(customEmojiIcon.Emoji?.Url);
          break;
        case "external":
          var externalIcon = (ExternalIcon)notionPage.Icon;
          icon = new(externalIcon.External?.Url);
          break;
      }
    }

    Title = title;
    Icon = icon;

  }
}