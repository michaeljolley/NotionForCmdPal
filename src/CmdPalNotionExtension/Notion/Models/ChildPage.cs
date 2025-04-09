using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record ChildPage(
  [property: JsonPropertyName("title")] string? Title
);