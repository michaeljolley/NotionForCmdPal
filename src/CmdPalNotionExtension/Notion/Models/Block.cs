using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BlockBookmark), typeDiscriminator: "bookmark")]
[JsonDerivedType(typeof(BlockBreadcrumb), typeDiscriminator: "breadcrumb")]
[JsonDerivedType(typeof(BlockBulletedListItem), typeDiscriminator: "bulleted_list_item")]
[JsonDerivedType(typeof(BlockCallout), typeDiscriminator: "callout")]
[JsonDerivedType(typeof(BlockChildDatabase), typeDiscriminator: "child_database")]
[JsonDerivedType(typeof(BlockChildPage), typeDiscriminator: "child_page")]
[JsonDerivedType(typeof(BlockCode), typeDiscriminator: "code")]
[JsonDerivedType(typeof(BlockDivider), typeDiscriminator: "divider")]
[JsonDerivedType(typeof(BlockEmbed), typeDiscriminator: "embed")]
[JsonDerivedType(typeof(BlockEquation), typeDiscriminator: "equation")]
[JsonDerivedType(typeof(BlockFile), typeDiscriminator: "file")]
[JsonDerivedType(typeof(BlockHeading), typeDiscriminator: "heading_1")]
[JsonDerivedType(typeof(BlockHeading), typeDiscriminator: "heading_2")]
[JsonDerivedType(typeof(BlockHeading), typeDiscriminator: "heading_3")]
[JsonDerivedType(typeof(BlockImage), typeDiscriminator: "image")]
[JsonDerivedType(typeof(BlockNumberedListItem), typeDiscriminator: "numbered_list_item")]
[JsonDerivedType(typeof(BlockParagraph), typeDiscriminator: "paragraph")]
[JsonDerivedType(typeof(BlockPDF), typeDiscriminator: "pdf")]
[JsonDerivedType(typeof(BlockQuote), typeDiscriminator: "quote")]
internal abstract record Block
{
  [JsonPropertyName("object")]
  public string? Object { get; init; }

  [JsonPropertyName("id")]
  public string? Id { get; init; }

  [JsonPropertyName("parent")]
  public ParentId? Parent { get; init; }

  [JsonPropertyName("type")]
  public string? Type { get; init; }

  [JsonPropertyName("created_time")]
  public System.DateTime? CreatedTime { get; init; }

  [JsonPropertyName("created_by")]
  public User? CreatedBy { get; init; }

  [JsonPropertyName("last_edited_time")]
  public System.DateTime? LastEditedTime { get; init; }

  [JsonPropertyName("last_edited_by")]
  public User? LastEditedBy { get; init; }

  [JsonPropertyName("archived")]
  public bool? Archived { get; init; }

  [JsonPropertyName("in_trash")]
  public bool? InTrash { get; init; }

  [JsonPropertyName("has_children")]
  public bool? HasChildren { get; init; }

}

