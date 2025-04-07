using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record LinkPreview(
  [property: JsonPropertyName("url")] string? Url
);