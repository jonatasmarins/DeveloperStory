using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.App.Models.Queries.Cart.Request;
using DeveloperStore.App.Models.Queries.Cart.Response;
using DeveloperStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace DeveloperStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IMediator mediator, ILogger<CartController> logger) : Controller
    {        
        [HttpGet]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Get Carts")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllCartQueryResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllCartQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                
                if (!result.Success) return StatusCode((int)result.StatusCode, new ResourceNotFoundResultResponse(detail: string.Join(", ", result.GetMessages())));

                return Response(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }


        [HttpGet("{Id}")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Get Carts by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetByIdCartQueryResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCartQueryRequest request)
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
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Add Cart")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AddCartCommandResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddCartCommandRequest request)
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
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Update Cart")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UpdateCartCommandResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCartCommandRequest request)
        {
            try
            {
                request.Id = id;

                var result = await mediator.Send(request);

                if (!result.Success)
                {
                    var detail = string.Join(", ", result.GetMessages());

                    ErrorResultResponse erroResult = new ValidationErrorResultResponse(detail: detail);

                    if (result.StatusCode == HttpStatusCode.NotFound) erroResult = new ResourceNotFoundResultResponse($"{nameof(Cart)} not found", detail);

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
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Delete Cart")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DeleteCartCommandResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] DeleteCartCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ResourceNotFoundResultResponse($"{nameof(Cart)} not found", string.Join(", ", result.GetMessages())));

                return Ok(result.Data.Message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }
    }
}