using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotionForCmdPalOAuthAPI.Authentication;

namespace NotionForCmdPalOAuthAPI.Functions;

public class Authorize
{
  private readonly ILogger<Authorize> _logger;

  public Authorize(ILogger<Authorize> logger)
  {
    _logger = logger;
  }

  [Function("Authorize")]
  public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
  {
    _logger.LogInformation("Authorize function processed a request.");
    var codeUrl = await OAuthClient.CreateOAuthCodeRequestUriAsync();
    return new RedirectResult(codeUrl.ToString());
  }
}
