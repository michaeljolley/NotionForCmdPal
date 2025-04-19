using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record DatabaseTitle(
  [property: JsonPropertyName("type")] string? Type,
  [property: JsonPropertyName("text")] string? Text,
  [property: JsonPropertyName("annotations")] DatabaseAnnotations? Annotations,
  [property: JsonPropertyName("plain_text")] string? PlainText,
  [property: JsonPropertyName("href")] string? Href
);
