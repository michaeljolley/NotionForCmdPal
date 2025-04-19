using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DatabaseParentId), typeDiscriminator: "database_id")]
[JsonDerivedType(typeof(PageParentId), typeDiscriminator: "page_id")]
[JsonDerivedType(typeof(BlockParentId), typeDiscriminator: "block_id")]
[JsonDerivedType(typeof(WorkspaceParentId), typeDiscriminator: "workspace")]
internal abstract record ParentId
{
  [JsonPropertyName("type")]
  string? Type { get; init; }
}
