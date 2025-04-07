using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record ExternalObject(
  [property: JsonPropertyName("url")] string? Url
);
