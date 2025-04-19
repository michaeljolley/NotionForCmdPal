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
  [property: JsonPropertyName("cover")] ImageRef? Cover,
  [property: JsonPropertyName("icon")] ImageRef? Icon,
  [property: JsonPropertyName("parent")] object? Parent,
  [property: JsonPropertyName("archived")] bool? IsArchived,
  [property: JsonPropertyName("in_trash")] bool? InTrash,
  [property: JsonPropertyName("properties")] Dictionary<string, Property>? Properties,
  [property: JsonPropertyName("url")] string? Url,
  [property: JsonPropertyName("public_url")] string? PublicUrl
);
