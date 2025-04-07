using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record UserCollection(
  [property: JsonPropertyName("results")] List<User>? Results,
  [property: JsonPropertyName("next_cursor")] string? NextCursor,
  [property: JsonPropertyName("has_more")] bool? HasMore
);
