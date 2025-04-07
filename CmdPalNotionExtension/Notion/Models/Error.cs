using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Error(
  [property: JsonPropertyName("code")] string? Code,
  [property: JsonPropertyName("message")] string? Message
);
