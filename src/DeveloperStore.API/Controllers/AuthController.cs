using DeveloperStore.App.Models.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

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
