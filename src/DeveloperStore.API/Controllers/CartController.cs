using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models.Queries.Cart.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeveloperStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IMediator mediator, ILogger<CartController> logger) : Controller
    {
        //TODO - Documentar as apis com swagger

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCartQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (result is null || !result.Data.Any()) return NotFound(result);

                return Response(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCartQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (result is null || result.Id == 0) return NotFound(result);

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
        public async Task<IActionResult> Add([FromBody] AddCartCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return BadRequest(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }


        [HttpPut($"{{id}}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCartCommandRequest request)
        {
            try
            {
                request.Id = id;

                var result = await mediator.Send(request);

                if (result is null || result.Data.Id == 0) return NotFound(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteCartCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, string.Join(",", result.GetMessages()));

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