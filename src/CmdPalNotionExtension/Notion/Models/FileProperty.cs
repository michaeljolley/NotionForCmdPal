using System;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(FileObject), typeDiscriminator: "file")]
[JsonDerivedType(typeof(ExternalObject), typeDiscriminator: "external")]
internal sealed record FileProperty(
  [property: JsonPropertyName("type")] string? Type
);
