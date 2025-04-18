using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Bookmark(
  [property: JsonPropertyName("caption")] System.Collections.Generic.List<RichText>? Caption,
  [property: JsonPropertyName("url")] string? Url
);

