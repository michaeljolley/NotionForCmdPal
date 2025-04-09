using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record DatabaseText(
  [property: JsonPropertyName("content")] string? Content,
  [property: JsonPropertyName("link")] string? Link
);
