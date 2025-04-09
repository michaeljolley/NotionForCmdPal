using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record PageParentId(
  [property: JsonPropertyName("page_id")] string? PageId
) : ParentId;
