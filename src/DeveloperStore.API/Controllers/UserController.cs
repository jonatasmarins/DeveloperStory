using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.User.Request;
using DeveloperStore.App.Models.Queries.User.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using DeveloperStore.App.Models.Queries.User.Response;
using DeveloperStore.App.Models.Commands.User.Response;

namespace DeveloperStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, ILogger<UserController> logger) : Controller
    {
        [HttpGet]
        [Authorize(Policy = "ManagerOrAdm", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Get all users")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllUserQueryResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllUserQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ValidationErrorResultResponse(detail: string.Join(", ", result.GetMessages())));

                return Response(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }


        [HttpGet("{Id}")]
        [Authorize(Policy = "ManagerOrAdm", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Get user by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetByIdUserQueryResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ValidationErrorResultResponse(detail: string.Join(", ", result.GetMessages())));

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        [HttpPost]
        [AllowAnonymous]        
        [SwaggerOperation(Summary = "Add new user")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetByIdUserQueryResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]                        
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddUserCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ValidationErrorResultResponse(detail: string.Join(", ", result.GetMessages())));

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }


        [HttpPut($"{{id}}")]
        [Authorize(Roles = "All", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Update user")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetByIdUserQueryResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserCommandRequest request)
        {
            try
            {
                request.Id = id;

                var result = await mediator.Send(request);

                if (!result.Success)
                {
                    var detail = string.Join(", ", result.GetMessages());

                    ErrorResultResponse erroResult = new ValidationErrorResultResponse(detail: detail);

                    if (result.StatusCode == HttpStatusCode.NotFound) erroResult = new ResourceNotFoundResultResponse("User not found", detail);

                    return StatusCode((int)result.StatusCode, erroResult);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "All", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Delete user by Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeleteUserCommandResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ResourceNotFoundResultResponse("User not found", string.Join(", ", result.GetMessages())));

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
