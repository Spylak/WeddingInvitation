using Microsoft.AspNetCore.Mvc;

namespace InvitationAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IConfiguration _configuration;
    public AdminController(ILogger<AdminController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public string GetToken(string usernameInput,string passwordInput)
    {
        var username=_configuration.GetValue<string>("AdminCreds:Username");
        var password=_configuration.GetValue<string>("AdminCreds:Password");
        if (username == usernameInput && passwordInput == password)
        {
            return "Success";
        }
        return "Unauthorized";
    }
}