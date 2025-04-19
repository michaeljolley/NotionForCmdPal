using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record WorkspaceParentId(
  [property: JsonPropertyName("workspace")] bool? Workspace
) : ParentId;

