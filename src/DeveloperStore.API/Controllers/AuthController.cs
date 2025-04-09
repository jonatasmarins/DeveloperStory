using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Auth;
using DeveloperStore.App.Models.Queries.User.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [HttpPost("Login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Login")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetByIdUserQueryResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(AuthenticationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Token(LoginCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);                

                if (!result.Success) return StatusCode((int)result.StatusCode, new AuthenticationErrorResultResponse($"Authentication invalid", string.Join(", ", result.GetMessages())));

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }
    }
}
