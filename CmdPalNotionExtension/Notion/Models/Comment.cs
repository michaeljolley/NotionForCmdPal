using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record Comment(
  [property: JsonPropertyName("parent")] ParentId? Parent,
  [property: JsonPropertyName("discussion_id")] string? DiscussionId,
  [property: JsonPropertyName("rich_text")] RichText? RichText
);
