using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockBookmark(
  [property: JsonPropertyName("bookmark")] Bookmark? Bookmark
) : Block;
