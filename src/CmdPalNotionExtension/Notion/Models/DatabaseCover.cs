using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record DatabaseCover(
  [property: JsonPropertyName("type")] string? Type,
  [property: JsonPropertyName("external")] object? External
);
