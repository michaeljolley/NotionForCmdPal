using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockFile(
  [property: JsonPropertyName("file")] File? File
) : Block;