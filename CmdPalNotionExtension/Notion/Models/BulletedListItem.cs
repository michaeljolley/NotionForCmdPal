using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BulletedListItem(
  [property: JsonPropertyName("rich_text")] List<BulletedListItem>? RichText,
  [property: JsonPropertyName("color")] string? Color,
  [property: JsonPropertyName("children")] List<Block>? Children
);
