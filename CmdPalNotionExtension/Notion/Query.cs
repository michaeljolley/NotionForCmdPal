using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion;

internal sealed partial record Query
{
  [JsonPropertyName("query")]
  public string? SearchFor { get; init; }
  
  [JsonPropertyName("sort")]
  public object? Sort { get; init; }

  [JsonPropertyName("filter")]
  public static object? Filter { get => new { direction = NotionSortDirection.DESC, timestamp = "last_edited_time" }; }

  [JsonPropertyName("start_cursor")]
  public object? Cursor { get; set; }

  [JsonPropertyName("page_size")]
  public object? PageSize { get; set; }
}

internal struct NotionSortDirection
{
  internal const string ASC = "ascending";
  internal const string DESC = "descending";
}

