using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Callout(
  [property: JsonPropertyName("rich_text")] List<RichText>? RichText,
  [property: JsonPropertyName("icon")] Icon? Icon,
  [property: JsonPropertyName("color")] string? Color
);
