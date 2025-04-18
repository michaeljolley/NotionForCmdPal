using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record RichTextText(
  [property: JsonPropertyName("text")] Text? Text
) : RichText;
