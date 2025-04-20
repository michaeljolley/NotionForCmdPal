using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using CmdPalNotionExtension.Authentication;
using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.Notion;

internal sealed partial class NotionDataProvider
{
  private readonly TokenService _tokenService;
  private readonly JsonSerializerOptions _jsonSerializerOptions = new() 
  { 
    WriteIndented = true, 
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    AllowOutOfOrderMetadataProperties = true
  };

  public NotionDataProvider(TokenService tokenService)
  {
    _tokenService = tokenService;
  }

  private HttpClient GetClient()
  {
    if (!_tokenService.IsSignedIn())
    {
      throw new InvalidOperationException("Not signed in to Notion");
    }

    var client = new HttpClient();
    client.BaseAddress = new Uri("https://api.notion.com");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");

    // Add users API key here
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.GetAccessToken());
    return client;
  }

  private async Task<T> SendAsync<T>(HttpRequestMessage request)
  {
    using var client = GetClient();

    try
    {
      var response = await client.SendAsync(request);
      var r = await response.Content.ReadAsStringAsync();

      response.EnsureSuccessStatusCode();

      return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error SendAsync: {ex.Message}");
      throw;
    }
  }

  public async Task<SearchResult> GetRecentNotionPagesAsync(string? cursor)
  {
    var query = new Query() { PageSize = 20 };

    if (!string.IsNullOrEmpty(cursor))
    {
      query.Cursor = cursor;
    }

    var payload = JsonSerializer.Serialize(query, _jsonSerializerOptions);

    var request = new HttpRequestMessage(HttpMethod.Post, "/v1/search")
    {
      Content = new StringContent(payload, Encoding.UTF8, "application/json")
    };

    var response = await SendAsync<SearchResult>(request);
    return response;
  }
}
