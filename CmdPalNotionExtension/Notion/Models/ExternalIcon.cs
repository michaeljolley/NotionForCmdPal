using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record ExternalIcon(
  [property: JsonPropertyName("external")] ExternalObject? External
) : Icon;
