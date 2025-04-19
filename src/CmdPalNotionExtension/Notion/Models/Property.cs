using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(
  TypeDiscriminatorPropertyName = "type",
  IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(Property), typeDiscriminator: "base")]
[JsonDerivedType(typeof(CheckboxProperty), typeDiscriminator: "checkbox")]
[JsonDerivedType(typeof(CreatedTimeProperty), typeDiscriminator: "created_time")]
[JsonDerivedType(typeof(DateProperty), typeDiscriminator: "date")]
[JsonDerivedType(typeof(EmailProperty), typeDiscriminator: "email")]
[JsonDerivedType(typeof(FilesProperty), typeDiscriminator: "files")]
[JsonDerivedType(typeof(FormulaProperty), typeDiscriminator: "formula")]
[JsonDerivedType(typeof(IconProperty), typeDiscriminator: "icon")]
[JsonDerivedType(typeof(LastEditedByProperty), typeDiscriminator: "last_edited_by")]
[JsonDerivedType(typeof(LastEditedTimeProperty), typeDiscriminator: "last_edited_time")]
[JsonDerivedType(typeof(MultiSelectProperty), typeDiscriminator: "multi_select")]
[JsonDerivedType(typeof(NumberProperty), typeDiscriminator: "number")]
[JsonDerivedType(typeof(PeopleProperty), typeDiscriminator: "people")]
[JsonDerivedType(typeof(PhoneNumberProperty), typeDiscriminator: "phone_number")]
[JsonDerivedType(typeof(RelationProperty), typeDiscriminator: "relation")]
[JsonDerivedType(typeof(RollupProperty), typeDiscriminator: "rollup")]
[JsonDerivedType(typeof(RichTextProperty), typeDiscriminator: "rich_text")]
[JsonDerivedType(typeof(SelectProperty), typeDiscriminator: "select")]
[JsonDerivedType(typeof(StatusProperty), typeDiscriminator: "status")]
[JsonDerivedType(typeof(TitleProperty), typeDiscriminator: "title")]
[JsonDerivedType(typeof(UrlProperty), typeDiscriminator: "url")]
[JsonDerivedType(typeof(UniqueIdProperty), typeDiscriminator: "unique_id")]
[JsonDerivedType(typeof(VerificationProperty), typeDiscriminator: "verification")]
internal record Property
{
  [property: JsonPropertyName("id")]
  public string? Id { get; init; }

  [property: JsonPropertyName("type")]
  public string? Type { get; init; }

  [property: JsonPropertyName("name")]
  public string? Name { get; init; }

  [property: JsonPropertyName("has_more")]
  public bool? HasMore { get; init; }
}
