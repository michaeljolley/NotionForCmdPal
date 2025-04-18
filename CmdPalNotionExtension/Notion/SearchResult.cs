using System.Collections.Generic;
using System.Text.Json.Serialization;

using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.Notion;

internal sealed partial class SearchResult
{
  [JsonPropertyName("results")]
  public required List<NotionPage> Results { get; set; }

  [JsonPropertyName("next_cursor")]
  public string? NextCursor { get; set; }
  
  [JsonPropertyName("has_more")]
  public bool HasMore { get; set; }
}
