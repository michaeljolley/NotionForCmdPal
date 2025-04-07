using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EmojiIcon), typeDiscriminator: "emoji")]
[JsonDerivedType(typeof(FileIcon), typeDiscriminator: "file")]
[JsonDerivedType(typeof(ExternalIcon), typeDiscriminator: "external")]
internal abstract record Icon
{
  [property: JsonPropertyName("type")]
  public string? Type { get; init; }
}

