using Bogus;
using DeveloperStore.API.Controllers;
using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;

namespace DeveloperStore.Tests.Controllers
{
    public class AuthControllerTest
    {
        private IMediator _mediator;
        private ILogger<AuthController> _logger;
        private AuthController _controller;

        public AuthControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _logger = Substitute.For<ILogger<AuthController>>();

            _controller = new AuthController(_mediator, _logger);
        }


        [Fact(DisplayName = "[Get] - Success")]
        [Trait("Get", "Success")]
        public async Task ItShould_GetToken_Sucess()
        {
            // Arrange
            var request = new Faker<LoginCommandRequest>("pt_BR")
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.Password, f => f.Person.Random.AlphaNumeric(10));

            var login = new Faker<LoginCommandResponse>()
                .RuleFor(x => x.Token, f => f.Random.Hash());

            var response = new Faker<ResultResponse<LoginCommandResponse>>()
                .RuleFor(x => x.Data, login)
                .Generate();

            _mediator.Send(Arg.Any<LoginCommandRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Token(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);            

            Assert.NotNull(okResult.Value);
        }

        [Fact(DisplayName = "[Get] - Fail")]
        [Trait("Get", "Fail")]
        public async Task ItShould_GetToken_Fail()
        {
            // Arrange
            string errorMessage = "Invalid credentials";

            var request = new Faker<LoginCommandRequest>("pt_BR")
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.Password, f => f.Person.Random.AlphaNumeric(10));

            var login = new Faker<LoginCommandResponse>()
                .RuleFor(x => x.Token, f => f.Random.Hash());

            var response = new Faker<ResultResponse<LoginCommandResponse>>()
                .RuleFor(x => x.Data, login)
                .RuleFor(x => x.StatusCode, HttpStatusCode.Unauthorized)
                .Generate();

            response.AddMessage(errorMessage);

            _mediator.Send(Arg.Any<LoginCommandRequest>(), default)
                .Returns(response);

            // Act
            var result = await _controller.Token(request);

            // Assert
            var unAuthorizedResult = Assert.IsType<ObjectResult>(result);
            
            Assert.Equal((int)HttpStatusCode.Unauthorized, unAuthorizedResult.StatusCode);
            
            Assert.Equal(errorMessage, unAuthorizedResult?.Value);
        }

    }
}
