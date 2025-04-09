using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;
using System.Net;
using NSubstitute.ExceptionExtensions;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class GetByIdQueryHandlerTest
    {
        private readonly ProductFixture _fixture;

        public GetByIdQueryHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Theory(DisplayName = "[GetById] - Not Found")]
        [Trait("GetById", "Not Found")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ItShould_GetProductById_NotFound(bool IsNull)
        {
            // Arrange
            var request = new GetByIdQueryRequest { Id = 1 };

            _fixture._productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(_fixture.GetProductEmptyOrNull(IsNull));

            // Act
            var result = await _fixture._GetById.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Contains($"Product with {request.Id} ID does not exist in our database", result.Erros);
        }

        [Fact(DisplayName = "[GetById] - Success")]
        [Trait("GetById", "Success")]
        public async Task ItShould_GetProductById_Success()
        {
            // Arrange
            var request = new GetByIdQueryRequest { Id = 1 };

            var product = new ProductFaker().Generate();
            
            _fixture._productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(product);             

            // Act
            var result = await _fixture._GetById.Handle(request, CancellationToken.None);

            // Assert            
            Assert.True(result.Success);            

            await _fixture._productRepository
                .Received(1)
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>());
        }

        [Fact(DisplayName = "[GetById] - Exception")]
        [Trait("GetById", "Exception")]
        public async Task ItShould_GetProductById_Exception()
        {
            // Arrange
            var request = new GetByIdQueryRequest { Id = 1 };

            _fixture._productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Throws(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture._GetById.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
