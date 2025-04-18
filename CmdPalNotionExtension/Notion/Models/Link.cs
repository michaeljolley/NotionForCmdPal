using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Link(
  [property: JsonPropertyName("url")] string? Url
);

