using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion;

internal sealed partial record Query
{
  [JsonPropertyName("query")]
  public string? SearchFor { get; init; }
  
  [JsonPropertyName("sort")]
  public static object? Sort { get => new { direction = NotionSortDirection.DESC, timestamp = "last_edited_time" }; }

  [JsonPropertyName("filter")]
  public object? Filter { get; init; }

  [JsonPropertyName("start_cursor")]
  public string? Cursor { get; set; }

  [JsonPropertyName("page_size")]
  public object? PageSize { get; set; }
}

internal struct NotionSortDirection
{
  internal const string ASC = "ascending";
  internal const string DESC = "descending";
}

