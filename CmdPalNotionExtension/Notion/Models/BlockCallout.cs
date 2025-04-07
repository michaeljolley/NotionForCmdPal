using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockCallout(
  [property: JsonPropertyName("callout")] Callout? Callout
) : Block;
