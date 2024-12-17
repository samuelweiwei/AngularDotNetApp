using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AngularDotNetApp.Server.Controllers;

[ApiController]
[EnableCors("FromAnyOrigin")]
[Route("[controller]")]
public class UserNameController : ControllerBase
{

    private readonly ILogger<UserNameController> _logger;

    public UserNameController(ILogger<UserNameController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "SimpleName")]
    public string Get(string firstName, string lastName)
    {
        _logger.LogInformation(firstName, lastName);
        return "hello world";
    }
}
