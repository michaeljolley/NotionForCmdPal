using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CustomEmojiIcon), typeDiscriminator: "custom_emoji")]
[JsonDerivedType(typeof(EmojiIcon), typeDiscriminator: "emoji")]
[JsonDerivedType(typeof(FileIcon), typeDiscriminator: "file")]
[JsonDerivedType(typeof(ExternalIcon), typeDiscriminator: "external")]
internal abstract record ImageRef
{
  [property: JsonPropertyName("type")]
  public string? Type { get; init; }
}

