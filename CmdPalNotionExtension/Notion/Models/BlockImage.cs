using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockImage(
  [property: JsonPropertyName("image")] FileObject? Image
) : Block;
