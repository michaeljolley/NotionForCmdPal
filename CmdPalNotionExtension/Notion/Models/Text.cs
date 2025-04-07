using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Text(
  [property: JsonPropertyName("content")] string? Content,
  [property: JsonPropertyName("link")] Link? Link
);
