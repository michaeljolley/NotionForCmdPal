using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record CustomEmojiIcon(
  [property: JsonPropertyName("custom_emoji")] CustomEmoji? Emoji
) : ImageRef;

internal sealed record CustomEmoji(
  [property: JsonPropertyName("id")] string? Id,
  [property: JsonPropertyName("name")] string? Name,
  [property: JsonPropertyName("url")] string? Url
);