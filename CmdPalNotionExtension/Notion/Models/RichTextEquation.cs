using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record RichTextEquation(
  [property: JsonPropertyName("equation")] Equation? Equation
) : RichText;
