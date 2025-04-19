using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockParagraph(
  [property: JsonPropertyName("paragraph")] Paragraph? Paragraph
) : Block;
