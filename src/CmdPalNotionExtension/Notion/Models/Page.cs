using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Page(
  [property: JsonPropertyName("object")] string? Object,
  [property: JsonPropertyName("id")] string? Id,
  [property: JsonPropertyName("created_time")] DateTime? CreatedTime,
  [property: JsonPropertyName("last_edited_time")] DateTime? LastEditedTime,
  [property: JsonPropertyName("created_by")] User? CreatedBy,
  [property: JsonPropertyName("last_edited_by")] User? LastEditedBy,
  [property: JsonPropertyName("cover")] string? Cover,
  [property: JsonPropertyName("icon")] string? Icon,
  [property: JsonPropertyName("parent")] object? Parent,
  [property: JsonPropertyName("in_trash")] bool? InTrash,
  [property: JsonPropertyName("properties")] Dictionary<string, object>? Properties,
  [property: JsonPropertyName("url")] string? Url,
  [property: JsonPropertyName("public_url")] string? PublicUrl
);
