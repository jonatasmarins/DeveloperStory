using AutoMapper;
using Bogus;
using DeveloperStore.API.Controllers;
using DeveloperStore.App.Models;
using DeveloperStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;
using DeveloperStore.App.Models.Queries.User.Request;
using DeveloperStore.App.Models.Queries.User.Response;
using NSubstitute.ExceptionExtensions;
using DeveloperStore.App.Models.Commands.User.Request;
using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.App.Mappers;
using DeveloperStore.Infra.Context.Identity;
using DeveloperStore.Domain.Enums;

namespace DeveloperStore.Tests.Controllers
{
    public class UserControllerTest
    {
        private IMediator _mediator;
        private ILogger<UserController> _logger;
        private IMapper _mapper;
        private UserController _controller;

        public UserControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _logger = Substitute.For<ILogger<UserController>>();

            CreateMapper();

            _controller = new UserController(_mediator, _logger);
        }

        #region GetAll

        [Fact(DisplayName = "[GetAll] - Not Found")]
        [Trait("GetAll", "NotFound")]
        public async Task ItShould_GetAll_NotFound()
        {
            // Arrange
            var request = new GetAllUserQueryRequest();

            var response = new ResultResponse<IEnumerable<GetAllUserQueryResponse>>()
            {
                Data = []
            };

            _mediator
                .Send(Arg.Any<GetAllUserQueryRequest>(), default)
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
            var request = new GetAllUserQueryRequest();
            var products = new Faker<GetAllUserQueryResponse>().Generate(5);

            var response = new ResultResponse<IEnumerable<GetAllUserQueryResponse>>()
            {
                Data = products,
                TotalItems = products.Count
            };


            _mediator
                .Send(Arg.Any<GetAllUserQueryRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Get(request);

            var data = ((OkObjectResult)result).Value as IResultResponse<IEnumerable<GetAllUserQueryResponse>>;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            Assert.True(data.TotalItems == products.Count);
        }

        [Fact(DisplayName = "[Get All] - Internal Server Error")]
        [Trait("GetAll", "InternalServerError")]
        public async Task ItShould_GetAll_InternalServerError()
        {
            // Arrange
            var request = new GetAllUserQueryRequest();

            _mediator
                .Send(Arg.Any<GetAllUserQueryRequest>(), default)
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
            var request = new GetByIdUserQueryRequest();

            var response = new GetByIdUserQueryResponse();

            _mediator
                .Send(Arg.Any<GetByIdUserQueryRequest>(), default)
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

            var request = new GetByIdUserQueryRequest() { Id = productId };
            var product = new Faker<GetByIdUserQueryResponse>().RuleFor(x => x.Id, productId);

            _mediator
                .Send(Arg.Any<GetByIdUserQueryRequest>(), default)
                .Returns(product);

            // Act
            var result = await _controller.GetById(request);

            var data = ((OkObjectResult)result).Value as GetByIdUserQueryResponse;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(data?.Id == productId);
        }

        [Fact(DisplayName = "[GetByID] - InternalServerError")]
        [Trait("GetById", "InternalServerError")]
        public async Task ItShould_GetById_InternalServerError()
        {
            // Arrange
            var request = new GetByIdUserQueryRequest();

            _mediator
                .Send(Arg.Any<GetByIdUserQueryRequest>(), default)
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
            var request = new DeleteUserCommandRequest { Id = 1 };

            var user = new Faker<DeleteUserCommandResponse>();

            var response = new ResultResponse<DeleteUserCommandResponse>(user);

            _mediator
                .Send(Arg.Any<DeleteUserCommandRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);
        }

        #endregion

        #region Put

        [Fact(DisplayName = "[Update] - Success")]
        [Trait("Update", "Success")]
        public async Task Put_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = new UpdateUserCommandRequest { Id = 1 };

            var cart = _mapper.Map<UpdateUserCommandResponse>(GenerateUser());

            var mockResponse = new ResultResponse<UpdateUserCommandResponse>()
            {
                Data = cart
            };

            _mediator
                .Send(Arg.Any<UpdateUserCommandRequest>(), default)
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
                cfg.AddProfile(new UserProfile());
            });


            _mapper = configuration.CreateMapper();
        }

        private static ApplicationUser GenerateUser()
        {
            var cartFaker = new Faker<ApplicationUser>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.Name, f => GenerateUserName(f))
                .RuleFor(c => c.Address, f => GenerateUserAddress(f))
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.UserName, f => f.Person.UserName)
                .RuleFor(c => c.Status, f => f.PickRandom<Status>())
                .RuleFor(c => c.Role, f => f.PickRandom<Role>());

            return cartFaker.Generate();
        }

        private static Address GenerateUserAddress(Faker f)
        {
            var faker = new Faker<Address>("pt_BR")
                .RuleFor(cp => cp.Street, f.Address.StreetName())
                .RuleFor(cp => cp.City, f.Address.City())
                .RuleFor(cp => cp.Zipcode, f.Address.ZipCode())
                .RuleFor(cp => cp.Number, f.Random.Int(1, 100));

            return faker.Generate();
        }

        private static Name GenerateUserName(Faker f)
        {
            var faker = new Faker<Name>()
                .RuleFor(cp => cp.Firstname, f.Person.FirstName)
                .RuleFor(cp => cp.Lastname, f.Person.LastName);

            return faker.Generate();
        }
    }
}
