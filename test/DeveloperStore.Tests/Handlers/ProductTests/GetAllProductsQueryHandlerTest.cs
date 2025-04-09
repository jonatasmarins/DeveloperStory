using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;
using System.Net;
using NSubstitute.ExceptionExtensions;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class GetAllProductsQueryHandlerTest
    {
        private readonly ProductFixture _fixture;

        public GetAllProductsQueryHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Fact(DisplayName = "[GetAllProduct] - NotFound")]
        [Trait("GetAllProduct", "NotFound")]
        public async Task ItShould_GetAllProduct_NotFound()
        {
            // Arrange
            var request = new GetAllQueryRequestFaker().Generate();

            var response = new PaginatedResult<IEnumerable<Product>> { Data = [] };

            _fixture._productRepository
                .GetAllAsync(Arg.Any<QueryOptions>())
                .Returns(response);

            // Act
            var result = await _fixture._GetAllProducts.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

            Assert.Contains("Product Not Found", result.Erros);
        }

        [Fact(DisplayName = "[GetAllProduct] - Success")]
        [Trait("GetAllProduct", "Success")]
        public async Task ItShould_GetAllProduct_Success()
        {
            // Arrange
            var products = new ProductFaker().Generate(5);

            var request = new GetAllQueryRequestFaker().Generate();

            _fixture._productRepository
                .GetAllAsync(Arg.Any<QueryOptions>())
                .Returns(new PaginatedResult<IEnumerable<Product>> { Data = products });            

            // Act
            var result = await _fixture._GetAllProducts.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);

            Assert.Equal(5, result.Data.Count());

            await _fixture._productRepository
                .Received(1)
                .GetAllAsync(Arg.Any<QueryOptions>());            
        }

        [Fact(DisplayName = "[GetAllProduct] - Exception")]
        [Trait("GetAllProduct", "Exception")]
        public async Task ItShould_GetAllProduct_Exception()
        {
            // Arrange
            var request = new GetAllQueryRequestFaker().Generate();

            _fixture._productRepository
                .GetAllAsync(Arg.Any<QueryOptions>())
                .Throws(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture._GetAllProducts.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
