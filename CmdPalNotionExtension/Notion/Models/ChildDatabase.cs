using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record ChildDatabase(
  [property: JsonPropertyName("title")] string? Title
);
