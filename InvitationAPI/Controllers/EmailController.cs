using Microsoft.AspNetCore.Mvc;

namespace InvitationAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IConfiguration _configuration;
    public EmailController(ILogger<EmailController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> GetToken(string email)
    {
        
        return Ok();
    }
}