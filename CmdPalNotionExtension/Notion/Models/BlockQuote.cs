using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockQuote(
  [property: JsonPropertyName("quote")] Quote? Quote
) : Block;
