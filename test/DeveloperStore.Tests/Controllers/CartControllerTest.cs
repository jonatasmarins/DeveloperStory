using AutoMapper;
using Bogus;
using DeveloperStore.API.Controllers;
using DeveloperStore.App.Mappers;
using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.App.Models.Queries.Cart.Request;
using DeveloperStore.App.Models.Queries.Cart.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.App.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;

namespace DeveloperStore.Tests.Controllers
{
    public class CartControllerTeste
    {
        private IMediator _mediator;
        private ILogger<CartController> _logger;
        private IMapper _mapper;
        private CartController _controller;

        public CartControllerTeste()
        {
            _mediator = Substitute.For<IMediator>();
            _logger = Substitute.For<ILogger<CartController>>();

            CreateMapper();

            _controller = new CartController(_mediator, _logger);
        }

        #region GetAll

        [Fact(DisplayName = "[GetAll] - Not Found")]
        [Trait("GetAll", "NotFound")]
        public async Task ItShould_GetAll_NotFound()
        {
            // Arrange
            var request = new GetAllCartQueryRequest();

            var response = new ResultResponse<IEnumerable<GetAllCartQueryResponse>>()
            {
                Data = []
            };

            _mediator
                .Send(Arg.Any<GetAllCartQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Get(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "[GetAll] - Success")]
        [Trait("GetAll", "Success")]
        public async Task ItShould_GetAll_Success()
        {
            // Arrange
            var request = new GetAllCartQueryRequest();
            var products = new Faker<GetAllCartQueryResponse>().Generate(5);

            var response = new ResultResponse<IEnumerable<GetAllCartQueryResponse>>()
            {
                Data = products,
                TotalItems = products.Count
            };


            _mediator
                .Send(Arg.Any<GetAllCartQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Get(request);

            var data = ((OkObjectResult)result).Value as IResultResponse<IEnumerable<GetAllCartQueryResponse>>;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            Assert.True(data.TotalItems == products.Count);
        }

        [Fact(DisplayName = "[Get All] - Internal Server Error")]
        [Trait("GetAll", "InternalServerError")]
        public async Task ItShould_GetAll_InternalServerError()
        {
            // Arrange
            var request = new GetAllCartQueryRequest();

            _mediator
                .Send(Arg.Any<GetAllCartQueryRequest>(), default)
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
            var request = new GetByIdCartQueryRequest();

            var response = new GetByIdCartQueryResponse();

            _mediator
                .Send(Arg.Any<GetByIdCartQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.GetById(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "[GetByID] - Success")]
        [Trait("GetById", "Success")]
        public async Task ItShould_GetById_Success()
        {
            // Arrange
            var faker = new Faker();
            var productId = faker.Random.Int(1, 5);

            var request = new GetByIdCartQueryRequest() { Id = productId };
            var product = new Faker<GetByIdCartQueryResponse>().RuleFor(x => x.Id, productId);

            _mediator
                .Send(Arg.Any<GetByIdCartQueryRequest>(), default)
                .Returns(product);

            // Act
            var result = await _controller.GetById(request);

            var data = ((OkObjectResult)result).Value as GetByIdCartQueryResponse;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(data?.Id == productId);
        }

        [Fact(DisplayName = "[GetByID] - InternalServerError")]
        [Trait("GetById", "InternalServerError")]
        public async Task ItShould_GetById_InternalServerError()
        {
            // Arrange
            var request = new GetByIdCartQueryRequest();

            _mediator
                .Send(Arg.Any<GetByIdCartQueryRequest>(), default)
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetById(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

        #endregion

        #region Delete

        [Fact(DisplayName = "[Delete] - Success")]
        [Trait("Delete", "Success")]
        public async Task ItShould_Delete_Success()
        {
            // Arrange
            var request = new DeleteCartCommandRequest { Id = 1 };

            var response = new ResultResponse<DeleteCartCommandResponse>(new DeleteCartCommandResponse("Product Deleted with sucess"));

            _mediator
                .Send(Arg.Any<DeleteCartCommandRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(response.Data.Message, actionResult.Value);
        }

        #endregion

        #region Put

        [Fact(DisplayName = "[Update] - Success")]
        [Trait("Update", "Success")]
        public async Task Put_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = new UpdateCartCommandRequest { Id = 1 };

            var cart = _mapper.Map<UpdateCartCommandResponse>(GenerateCart());

            var mockResponse = new ResultResponse<UpdateCartCommandResponse>()
            {
                Data = cart
            };

            _mediator
                .Send(Arg.Any<UpdateCartCommandRequest>(), default)
                .Returns(mockResponse);

            // Act
            var result = await _controller.Put(1, request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);

            Assert.Equal(mockResponse.Data, actionResult.Value);
        }

        #endregion

        #region Add
        #endregion

        private void CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CartProfile());
            });


            _mapper = configuration.CreateMapper();
        }

        private static Cart GenerateCart()
        {
            var cartFaker = new Faker<Cart>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.Date, f => f.Date.Past())
                .RuleFor(c => c.UserId, f => f.Random.Int(1, 1000))
                .RuleFor(c => c.Products, f => GenerateCartProducts(f));

            return cartFaker.Generate();
        }

        private static ICollection<CartProduct> GenerateCartProducts(Faker f)
        {
            var cartProductFaker = new Faker<CartProduct>()
                .RuleFor(cp => cp.CartId, f => f.IndexFaker + 1)
                .RuleFor(cp => cp.ProductId, f => f.Random.Int(1, 100))
                .RuleFor(cp => cp.Quantity, f => f.Random.Int(1, 10));

            return cartProductFaker.Generate(f.Random.Int(1, 5));
        }
    }
}
