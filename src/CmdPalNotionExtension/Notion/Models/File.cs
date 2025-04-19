using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record File(
  [property: JsonPropertyName("caption")] List<RichText>? Caption,
  [property: JsonPropertyName("file")] FileObject? FileObj,
  [property: JsonPropertyName("name")] string? Name
);
