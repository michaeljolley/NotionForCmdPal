using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record NumberedListItem(
  [property: JsonPropertyName("rich_text")] List<RichText>? RichText,
  [property: JsonPropertyName("color")] string? Color,
  [property: JsonPropertyName("children")] List<Block>? Children
);
