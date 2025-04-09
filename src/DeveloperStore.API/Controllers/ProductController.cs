using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
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
    public class ProductController(IMediator mediator, ILogger<ProductController> logger) : Controller
    {
        [AllowAnonymous]
        [HttpGet(Name = "GetAllProduct")]
        [SwaggerOperation(Summary = "List all Products")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IResultResponse<IEnumerable<GetProductsQueryResponse>>))]            
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> Get([FromQuery] GetAllQueryRequest request)
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

        [AllowAnonymous]
        [HttpGet("{Id}", Name = "GetProductById")]
        [SwaggerOperation(Summary = "Get Product by Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetProductsQueryResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> GetById([FromRoute] GetByIdQueryRequest request)
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

        [HttpPost()]
        [Authorize(Policy = "ManagerOrAdm", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Add new product")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AddProductCommandResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddProductCommandRequest request)
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

        
        [HttpPut($"{{id}}", Name = "UpdateProduct")]
        [Authorize(Policy = "ManagerOrAdm", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Update product")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UpdateProductCommandResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResultResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductCommandRequest request)
        {
            try
            {
                request.Id = id;

                var result = await mediator.Send(request);

                if (!result.Success)
                {
                    var detail = string.Join(", ", result.GetMessages());

                    ErrorResultResponse erroResult = new ValidationErrorResultResponse(detail: detail);

                    if (result.StatusCode == HttpStatusCode.NotFound) erroResult = new ResourceNotFoundResultResponse($"{nameof(Product)} not found", detail);

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
        
        [HttpDelete("{Id}", Name = "Delete Product")]
        [Authorize(Policy = "ManagerOrAdm", AuthenticationSchemes = "Bearer")]
        [SwaggerOperation(Summary = "Delete product")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DeleteProductCommandResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ResourceNotFoundResultResponse($"{nameof(Product)} not found", string.Join(", ", result.GetMessages())));

                return Ok(result.Data.Message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        [AllowAnonymous]
        [HttpGet("Categories")]
        [SwaggerOperation(Summary = "List all Categories of products")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IResultResponse<IEnumerable<string>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await mediator.Send(new GetAllCategoriesQueryRequest());
                
                if (!result.Success) return StatusCode((int)result.StatusCode, new ResourceNotFoundResultResponse(detail: string.Join(", ", result.GetMessages())));

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        [AllowAnonymous]
        [HttpGet("Category/{Category}")]
        [SwaggerOperation(Summary = "Get category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IResultResponse<IEnumerable<GetByCategoryQueryResponse>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResourceNotFoundResultResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCategory([FromRoute] GetByCategoryQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, new ResourceNotFoundResultResponse(detail: string.Join(", ", result.GetMessages())));

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }
    }
}
