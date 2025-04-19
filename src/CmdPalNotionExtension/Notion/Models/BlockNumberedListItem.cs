using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockNumberedListItem(
  [property: JsonPropertyName("numbered_list_item")] NumberedListItem? NumberedListItem
) : Block;
