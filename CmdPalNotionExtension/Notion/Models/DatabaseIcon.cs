using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record DatabaseIcon(
  [property: JsonPropertyName("type")] string? Type,
  [property: JsonPropertyName("emoji")] string? Emoji
);
