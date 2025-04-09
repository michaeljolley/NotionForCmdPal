using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(RichTextEquation), typeDiscriminator: "equation")]
[JsonDerivedType(typeof(RichTextText), typeDiscriminator: "text")]
internal abstract record RichText
{
  [JsonPropertyName("type")]
  public string? Type { get; init; }

  [JsonPropertyName("annotations")]
  public RichTextAnnotation? Annotations { get; init; }

  [JsonPropertyName("plain_text")]
  public string? PlainText { get; init; }

  [JsonPropertyName("href")]
  public string? Href { get; init; }
}

