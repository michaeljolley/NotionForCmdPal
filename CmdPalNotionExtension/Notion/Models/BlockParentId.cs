using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockParentId(
  [property: JsonPropertyName("block_id")] string? BlockId
) : ParentId;

