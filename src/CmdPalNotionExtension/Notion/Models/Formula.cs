using System;
using System.Text.Json.Serialization;

namespace CmdPalNotionExtension.Notion.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberFormula), typeDiscriminator: "number")]
[JsonDerivedType(typeof(DateFormula), typeDiscriminator: "date")]
[JsonDerivedType(typeof(StringFormula), typeDiscriminator: "string")]
[JsonDerivedType(typeof(BooleanFormula), typeDiscriminator: "boolean")]
internal abstract record Formula
{
  [property: JsonPropertyName("type")]
  public string? Type { get; init; }
}

internal sealed record NumberFormula(
  [property: JsonPropertyName("number")] decimal? Number
) : Formula;

internal sealed record StringFormula(
  [property: JsonPropertyName("string")] string? String
) : Formula;

internal sealed record DateFormula(
  [property: JsonPropertyName("date")] DateTime? Date
) : Formula;

internal sealed record BooleanFormula(
  [property: JsonPropertyName("boolean")] bool? Boolean
) : Formula;
