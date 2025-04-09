using System;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record FileObject(
  [property: JsonPropertyName("url")] string? Url,
  [property: JsonPropertyName("expiry_time")] DateTime? ExpiryTime
);
