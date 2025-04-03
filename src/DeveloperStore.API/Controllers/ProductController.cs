using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeveloperStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator, ILogger<ProductController> logger) : Controller
    {
        //TODO - Documentar as apis com swagger

        /// <summary>
        /// Retrieve a list of all products
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, result.Data);

                return Response(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        /// <summary>
        /// Retrieve a specific product by ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, result.Data);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
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


        [HttpPut($"{{id}}")]
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

        // DELETE api/<ProductController>/5
        [HttpDelete("{Id}")]
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

        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await mediator.Send(new GetAllCategoriesQueryRequest());

                if (!result.Success) return StatusCode((int)result.StatusCode, result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred, please try again or contact the administrator");
            }
        }

        [HttpGet("Category/{Category}")]
        public async Task<IActionResult> GetByCategory([FromRoute] GetByCategoryQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                if (!result.Success) return StatusCode((int)result.StatusCode, result);

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
