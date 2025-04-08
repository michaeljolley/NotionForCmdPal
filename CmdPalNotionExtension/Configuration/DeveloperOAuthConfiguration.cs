using System;

namespace CmdPalNotionExtension.Configuration;

internal static class DeveloperOAuthConfiguration
{
  //// Follow this link https://developers.notion.com/docs/authorization#how-to-make-a-public-integration
  //// to create a Notion public integration (with RedirectUri = "cmdpalnotionext://oauth_redirect_uri/").
  //// The following info can be modified by setting the corresponding environment variables.
  //// How to set the environment variables:
  ////
  ////        On an elevated cmd window:
  ////                       setx NOTION_CLIENT_ID "Your public integration's ClientId" /m
  ////                       setx NOTION_CLIENT_SECRET "Your public integration's ClientSecret" /m

  // Notion public integration Client ID and Secret values should not be checked in. Rather than modifying these values,
  // setting the environment variables like shown above will persist across branch switches.
  internal static readonly string? ClientID = Environment.GetEnvironmentVariable("NOTION_CLIENT_ID");

  internal static readonly string? ClientSecret = Environment.GetEnvironmentVariable("NOTION_CLIENT_SECRET");
}