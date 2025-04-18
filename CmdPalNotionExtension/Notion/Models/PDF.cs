using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PDFExternal), typeDiscriminator: "external")]
[JsonDerivedType(typeof(PDFFile), typeDiscriminator: "file")]
internal abstract record PDF
{
  [property: JsonPropertyName("caption")]
  List<RichText>? Caption { get; init; }

  [property: JsonPropertyName("type")]
  string? Type { get; init; }
}
