using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockPDF(
  [property: JsonPropertyName("pdf")] PDF? Pdf
) : Block;

