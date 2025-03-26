using DeveloperStore.App.Models.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IMediator mediator,
        ILogger<AuthController> logger
    ) : Controller
    {
        //TODO - Documentar as apis com swagger

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Token(LoginCommandRequest request)
        {
            var result = await mediator.Send(request);

            if (!result.Success) return StatusCode((int)result.StatusCode, result.Erros);

            return Ok(result.Data); // no need to cast here because user.id is already a guid, and not a string
        }
    }
}
