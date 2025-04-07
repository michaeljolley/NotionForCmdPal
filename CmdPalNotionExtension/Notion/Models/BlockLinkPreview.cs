using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockLinkPreview(
  [property: JsonPropertyName("link_preview")] LinkPreview? LinkPreview
) : Block;
