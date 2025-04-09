using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockCode(
    [property: JsonPropertyName("code")] Code? Code 
) : Block;
