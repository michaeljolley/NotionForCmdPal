using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion;

internal sealed partial record Filter(
  [property:JsonPropertyName("value")] string Value = NotionFilterObject.PAGE,
  [property:JsonPropertyName("property")] string Property = "object"
);

internal struct NotionFilterObject
{
  internal const string DATABASE = "database";
  internal const string PAGE = "page";
}
