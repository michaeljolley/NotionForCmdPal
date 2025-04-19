using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record EmojiIcon(
  [property: JsonPropertyName("emoji")] string? Emoji
) : ImageRef;
