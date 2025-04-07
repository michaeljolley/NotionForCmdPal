using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockEquation(
  [property: JsonPropertyName("equation")] Equation? Equation
) : Block;
