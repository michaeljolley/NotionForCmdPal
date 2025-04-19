using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record User(
  [property: JsonPropertyName("object")] string? Object,
  [property: JsonPropertyName("id")] string? Id
);
