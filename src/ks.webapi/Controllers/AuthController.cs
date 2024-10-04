using System.Threading.Tasks;
using ks.application.Models.Auths.Request;
using ks.application.Models.Auths.Response;
using ks.application.Models.Response;
using ks.application.Models.Users;
using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel loginModel)
    {
        var result = await authService.LoginAsync(loginModel,
            cancellationToken: default);

        return Ok(result is not null
            ? result
            : throw new Exception("invalid email/password"));
    }
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] UserCreateModel registerModel)
    {
        var result = await authService.CreateUserAsync(model: registerModel,
            cancellationToken: default);

        return Ok(result is not null
            ? result
            : throw new Exception("create failed"));
    }
}