using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockBulletedListItem(
  [property: JsonPropertyName("bulleted_list_item")] BulletedListItem? BulletedListItem
) : Block;
