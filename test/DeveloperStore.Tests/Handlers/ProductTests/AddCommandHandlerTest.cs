using DeveloperStore.Domain.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class AddCommandHandlerTest
    {
        private readonly ProductFixture _fixture;

        public AddCommandHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Fact(DisplayName = "[Add] - Bad Request")]
        [Trait("Add", "BadRequest")]
        public async Task ItShould_AddProduct_BadRequest()
        {
            // Arrange
            var request = new AddProductCommandRequestFaker().Generate();

            request.Title = string.Empty;

            // Act
            var result = await _fixture._Add.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            Assert.Contains($"{nameof(request.Title)} is Required", result.Erros);
        }

        [Fact(DisplayName = "[Add] - Success")]
        [Trait("Add", "Success")]
        public async Task ItShould_AddProduct_Success()
        {
            // Arrange
            var request = new AddProductCommandRequestFaker().Generate();

            var product = new ProductFaker().Generate();
            
            _fixture._productRepository.AddAsync(Arg.Any<Product>()).Returns(product);

            // Act
            var result = await _fixture._Add.Handle(request, CancellationToken.None);

            // Assert
            await _fixture._productRepository
                .Received(1)
                .AddAsync(Arg.Is<Product>(p => p.Title == request.Title && p.Price == request.Price));

            await _fixture._unitOfWork.Received(1).SaveAsync(CancellationToken.None);

            Assert.True(result.Success);
        }
        
        [Fact(DisplayName = "[Add] - Internal Error")]
        [Trait("Add", "Internal Error")]
        public async Task ItShould_AddProduct_Exception()
        {
            // Arrange
            var request = new AddProductCommandRequestFaker().Generate();
            
            _fixture._productRepository.AddAsync(Arg.Any<Product>()).Throws(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture._Add.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
