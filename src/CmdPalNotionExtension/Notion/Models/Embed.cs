using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Embed(
  [property: JsonPropertyName("url")] string? Url
);
