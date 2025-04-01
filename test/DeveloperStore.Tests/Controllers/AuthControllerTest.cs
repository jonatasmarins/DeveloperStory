using DeveloperStore.API.Controllers;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DeveloperStore.Tests.Controllers
{
    public class AuthControllerTest
    {
        private IMediator _mediator;
        private ILogger<ProductController> _logger;
        private ProductController _controller;

        public AuthControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _logger = Substitute.For<ILogger<ProductController>>();

            _controller = new ProductController(_mediator, _logger);
        }
    }
}
