using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record BlockChildDatabase(
  [property: JsonPropertyName("child_database")] ChildDatabase? ChildDatabase
) : Block;
