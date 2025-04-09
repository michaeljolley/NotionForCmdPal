using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Equation(
  [property: JsonPropertyName("expression")] string? Expression
);