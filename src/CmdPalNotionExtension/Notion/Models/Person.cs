using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Person(
  [property: JsonPropertyName("email")] string? Email
);
