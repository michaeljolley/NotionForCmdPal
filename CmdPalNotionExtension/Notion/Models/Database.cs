using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Database(
  [property: JsonPropertyName("object")] string? Object,
  [property: JsonPropertyName("id")] string? Id,
  [property: JsonPropertyName("created_time")] DateTime? CreatedTime,
  [property: JsonPropertyName("last_edited_time")] DateTime? LastEditedTime,
  [property: JsonPropertyName("icon")] DatabaseIcon? Icon,
  [property: JsonPropertyName("cover")] DatabaseCover? Cover,
  [property: JsonPropertyName("url")] string? Url,
  [property: JsonPropertyName("title")] List<DatabaseTitle>? Title,
  [property: JsonPropertyName("description")] List<DatabaseTitle>? Description
);
