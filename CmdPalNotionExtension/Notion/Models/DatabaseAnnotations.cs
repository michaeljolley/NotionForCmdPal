using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record DatabaseAnnotations(
  [property: JsonPropertyName("bold")] bool? Bold,
  [property: JsonPropertyName("italic")] bool? Italic,
  [property: JsonPropertyName("strikethrough")] bool? Strikethrough,
  [property: JsonPropertyName("underline")] bool? Underline,
  [property: JsonPropertyName("code")] bool? Code,
  [property: JsonPropertyName("color")] string? Color
);
