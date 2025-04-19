using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record FileIcon(
  [property: JsonPropertyName("file")]
  FileObject? File
) : Icon;
