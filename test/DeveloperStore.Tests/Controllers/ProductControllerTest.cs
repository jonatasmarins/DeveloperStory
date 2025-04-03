using Bogus;
using DeveloperStore.API.Controllers;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.App.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;

namespace DeveloperStore.Tests.Controllers
{
    public class ProductControllerTest
    {
        private IMediator _mediator;
        private ILogger<ProductController> _logger;
        private ProductController _controller;

        public ProductControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _logger = Substitute.For<ILogger<ProductController>>();

            _controller = new ProductController(_mediator, _logger);
        }

        #region GetAll

        [Fact(DisplayName = "[Get All] - Not Found")]
        [Trait("GetAll", "NotFound")]
        public async Task ItShould_GetAll_NotFound()
        {
            // Arrange
            var request = new GetAllQueryRequest();

            var response = new ResultResponse<IEnumerable<GetProductsQueryResponse>>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = Enumerable.Empty<GetProductsQueryResponse>(),                
            };

            response.AddMessage("Products Not Found");

            _mediator
                .Send(Arg.Any<GetAllQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Get(request);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "[Get All] - Success")]
        [Trait("GetAll", "Success")]
        public async Task ItShould_GetAll_Success()
        {
            // Arrange
            var request = new GetAllQueryRequest();
            var products = new Faker<GetProductsQueryResponse>().Generate(5);

            var response = new ResultResponse<IEnumerable<GetProductsQueryResponse>>()
            {
                Data = products,
                TotalItems = products.Count
            };


            _mediator
                .Send(Arg.Any<GetAllQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Get(request);

            var data = ((OkObjectResult)result).Value as IResultResponse<IEnumerable<GetProductsQueryResponse>>;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            Assert.True(data.TotalItems == products.Count);
        }

        [Fact(DisplayName = "[Get All] - Internal Server Error")]
        [Trait("GetAll", "InternalServerError")]
        public async Task ItShould_GetAll_InternalServerError()
        {
            // Arrange
            var request = new GetAllQueryRequest();

            _mediator
                .Send(Arg.Any<GetAllQueryRequest>(), default)
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.Get(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

        #endregion

        #region GetByID

        [Fact(DisplayName = "[GetByID] - Not Found")]
        [Trait("GetById", "NotFound")]
        public async Task ItShould_GetByIdWhenProductNotExist_NotFound()
        {
            // Arrange
            var request = new GetByIdQueryRequest();

            var response = new ResultResponse<GetByIdQueryResponse>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Data = new GetByIdQueryResponse(),
            };

            response.AddMessage("Product Not Found");

            _mediator
                .Send(Arg.Any<GetByIdQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.GetById(request);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "[GetByID] - Success")]
        [Trait("GetById", "Success")]
        public async Task ItShould_GetById_Success()
        {
            // Arrange
            var faker = new Faker();
            var productId = faker.Random.Int(1, 5);

            var request = new GetByIdQueryRequest() { Id = productId };
            var product = new Faker<GetByIdQueryResponse>().RuleFor(x => x.Id, productId);

            _mediator
                .Send(Arg.Any<GetByIdQueryRequest>(), default)
                .Returns(new ResultResponse<GetByIdQueryResponse>(product));

            // Act
            var result = await _controller.GetById(request);

            var data = ((OkObjectResult)result).Value as GetByIdQueryResponse;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            Assert.True(data?.Id == productId);
        }

        [Fact(DisplayName = "[GetByID] - InternalServerError")]
        [Trait("GetById", "InternalServerError")]
        public async Task ItShould_GetById_InternalServerError()
        {
            // Arrange
            var request = new GetByIdQueryRequest();

            _mediator
                .Send(Arg.Any<GetByIdQueryRequest>(), default)
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetById(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

        #endregion

        #region GetAllCategories

        [Fact(DisplayName = "[GetAllCategories] - Not Found")]
        [Trait("GetAllCategories", "NotFound")]
        public async Task ItShould_GetAllCategories_NotFound()
        {
            // Arrange
            var request = new GetAllCategoriesQueryRequest();

            var response = new ResultResponse<IEnumerable<string>>();

            response.StatusCode = HttpStatusCode.NotFound;
            response.AddMessage("Product Not Found");

            _mediator
                .Send(Arg.Any<GetAllCategoriesQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.GetAllCategories();

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Theory(DisplayName = "[GetAllCategories] - Success")]
        [InlineData(5)]
        [Trait("GetAllCategories", "Success")]
        public async Task ItShould_GetAllCategories_Success(int qtd)
        {
            // Arrange
            var faker = new Faker("pt_BR");

            IEnumerable<string> categories = faker.Commerce.Categories(qtd);

            var response = new ResultResponse<IEnumerable<string>>
            {
                Data = categories
            };

            _mediator
                .Send(Arg.Any<GetAllCategoriesQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.GetAllCategories();

            var okResult = ((OkObjectResult)result).Value as ResultResponse<IEnumerable<string>>;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            Assert.True(okResult?.Data.Count() == qtd);
        }

        [Fact(DisplayName = "[GetAllCategories] - InternalServerError")]
        [Trait("GetAllCategories", "InternalServerError")]
        public async Task ItShould_GetAllCategories_InternalServerError()
        {
            // Arrange            
            _mediator
                .Send(Arg.Any<GetAllCategoriesQueryRequest>(), default)
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetAllCategories();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

        #endregion

        #region GetByCategory

        [Fact(DisplayName = "[GetByCategory] - Not Found")]
        [Trait("GetByCategory", "NotFound")]
        public async Task ItShould_GetByCategory_NotFound()
        {
            // Arrange
            var faker = new Faker("pt_BR");

            var category = faker.Commerce.Categories(1).First();

            var request = new GetByCategoryQueryRequest() { Category = category };

            var response = new ResultResponse<IEnumerable<GetByCategoryQueryResponse>>();

            response.StatusCode = HttpStatusCode.NotFound;

            response.AddMessage("Product Not Found");

            _mediator
                .Send(Arg.Any<GetByCategoryQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.GetByCategory(request);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Theory(DisplayName = "[GetByCategory] - Success")]
        [InlineData(5)]
        [Trait("GetByCategory", "Success")]
        public async Task ItShould_GetByCategory_Success(int qtd)
        {
            // Arrange
            var faker = new Faker("pt_BR");

            var category = faker.Commerce.Categories(1).First();

            var request = new GetByCategoryQueryRequest() { Category = category };

            var categories = new Faker<GetByCategoryQueryResponse>()
                                .RuleFor(x => x.Category, category)
                                .Generate(qtd);

            var response = new ResultResponse<IEnumerable<GetByCategoryQueryResponse>>() { Data = categories, TotalItems = qtd };

            _mediator
                .Send(Arg.Any<GetByCategoryQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.GetByCategory(request);

            var data = ((OkObjectResult)result).Value as ResultResponse<IEnumerable<GetByCategoryQueryResponse>>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(data?.Data.Count(), qtd);
        }

        [Fact(DisplayName = "[GetByCategory] - InternalServerError")]
        [Trait("GetByCategory", "InternalServerError")]
        public async Task ItShould_GetByCategory_InternalServerError()
        {
            // Arrange            
            _mediator
                .Send(Arg.Any<GetByCategoryQueryRequest>(), default)
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetByCategory(new GetByCategoryQueryRequest());

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

        #endregion
    }
}
