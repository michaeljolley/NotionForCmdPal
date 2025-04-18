using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record PDFExternal(
  [property: JsonPropertyName("external")] ExternalObject? External
) : PDF;
