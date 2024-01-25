using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Webhook.Pagamento.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    [HttpPost("subscribe")]
    public IActionResult Subscribe([FromBody] string callbackUrl)
    {
        Random random = new Random();
        int status = random.Next(1, 2);

        using var httpClient = new HttpClient();
        httpClient.PostAsJsonAsync(callbackUrl, status);

        return Ok();
    }
}
