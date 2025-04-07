using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Heading(
  [property: JsonPropertyName("rich_text")] List<RichText>? RichText,
  [property: JsonPropertyName("color")] string? Color,
  [property: JsonPropertyName("is_toggleable")] bool? IsToggleable
);
