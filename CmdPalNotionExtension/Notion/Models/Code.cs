using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Code(
  [property: JsonPropertyName("caption")] List<RichText>? Caption,
  [property: JsonPropertyName("rich_text")] List<RichText>? RichText,
  [property: JsonPropertyName("language")] string? Language
);
