using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

internal sealed record CheckboxProperty(
  [property: JsonPropertyName("checkbox")] bool IsChecked
) : Property;

internal sealed record CreatedTimeProperty(
  [property: JsonPropertyName("created_time")] DateTime CreatedAt
) : Property;

internal sealed record DateProperty(
  [property: JsonPropertyName("date")] DateType? DateObject
) : Property;

internal sealed record DateType(
  [property: JsonPropertyName("start")] DateTime? StartDate,
  [property: JsonPropertyName("end")] DateTime? EndDate,
  [property: JsonPropertyName("time_zone")] string? TimeZone
);

internal sealed record EmailProperty(
  [property: JsonPropertyName("email")] string? EmailAddress
) : Property;

internal sealed record FilesProperty(
  [property: JsonPropertyName("files")] FileProperty[]? FileArray
) : Property;

internal sealed record FormulaProperty(
  [property: JsonPropertyName("formula")] Formula? Formula
) : Property;

internal sealed record IconProperty(
  [property: JsonPropertyName("icon")] ImageRef? Icon
) : Property;

internal sealed record LastEditedByProperty(
  [property: JsonPropertyName("last_edited_by")] User User
) : Property;

internal sealed record LastEditedTimeProperty(
  [property: JsonPropertyName("last_edited_time")] DateTime LastEditedAt
) : Property;

internal sealed record MultiSelectProperty(
  [property: JsonPropertyName("multi_select")] List<MultiSelect>? MultiSelectArray
) : Property;

internal sealed record MultiSelect(
  [property: JsonPropertyName("id")] string Id,
  [property: JsonPropertyName("name")] string Name,
  [property: JsonPropertyName("color")] string Color
);

internal sealed record NumberProperty(
  [property: JsonPropertyName("format")] string? Format
) : Property;

internal sealed record PeopleProperty(
  [property: JsonPropertyName("people")] List<User>? People
) : Property;

internal sealed record PhoneNumberProperty(
  [property: JsonPropertyName("phone_number")] string? Number
) : Property;

internal sealed record RelationProperty(
  [property: JsonPropertyName("relations")] List<Relation>? Relations
) : Property;

internal sealed record Relation(
  [property: JsonPropertyName("id")] string? Id
);

internal sealed record RollupProperty(
) : Property;

internal sealed record RichTextProperty(
  [property: JsonPropertyName("rich_text")] List<RichText>? RichText
) : Property;

internal sealed record SelectProperty(
  [property: JsonPropertyName("select")] Select? Select
) : Property;

internal sealed record Select(
  [property: JsonPropertyName("id")] string Id,
  [property: JsonPropertyName("name")] string Name,
  [property: JsonPropertyName("color")] string Color
);

internal sealed record StatusProperty(
  [property: JsonPropertyName("status")] Status? Status
) : Property;

internal sealed record Status(
  [property: JsonPropertyName("id")] string Id,
  [property: JsonPropertyName("name")] string Name,
  [property: JsonPropertyName("color")] string Color
);

internal sealed record TitleProperty(
  [property: JsonPropertyName("title")] List<RichText>? TitleDetails
) : Property;

internal sealed record UrlProperty(
  [property: JsonPropertyName("url")] string? Url
) : Property;

internal sealed record UniqueIdProperty(
  [property: JsonPropertyName("url")] UniqueId? UniqueId
) : Property;

internal sealed record UniqueId(
  [property: JsonPropertyName("number")] int? Number,
  [property: JsonPropertyName("prefix")] string? Prefix
);

internal sealed record VerificationProperty(
  [property: JsonPropertyName("verification")] Verification? Verification
) : Property;

internal sealed record Verification(
  [property: JsonPropertyName("state")] string? State
);