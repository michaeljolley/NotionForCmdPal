using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotionForCmdPalOAuthAPI.Authentication;

namespace NotionForCmdPalOAuthAPI.Functions;

public class Token
{
  private readonly ILogger<Authorize> _logger;

  public Token(ILogger<Authorize> logger)
  {
    _logger = logger;
  }

  [Function("Token")]
  public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
  {
    _logger.LogInformation("Token function processed a request.");
    return await OAuthClient.HandleOAuthRedirection(new Uri(req.GetDisplayUrl())); 
  }
}
