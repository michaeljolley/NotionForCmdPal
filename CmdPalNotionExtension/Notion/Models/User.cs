using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record User(
  [property: JsonPropertyName("object")] string? Object,
  [property: JsonPropertyName("id")] string? Id,
  [property: JsonPropertyName("type")] string? Type,
  [property: JsonPropertyName("person")] Person? Person,
  [property: JsonPropertyName("name")] string? Name,
  [property: JsonPropertyName("avatar_url")] string? AvatarUrl
);
