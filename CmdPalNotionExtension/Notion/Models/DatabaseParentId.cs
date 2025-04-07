using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record DatabaseParentId(
  [property: JsonPropertyName("database_id")] string? DatabaseId
) : ParentId;
