using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record PDFFile(
  [property: JsonPropertyName("file")] FileObject? File
) : PDF;
