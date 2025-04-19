using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockChildPage(
  [property: JsonPropertyName("child_page")] ChildPage? ChildPage
) : Block;
