using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockHeading(
  [property: JsonPropertyName("heading_1")] Heading? Heading
) : Block;
