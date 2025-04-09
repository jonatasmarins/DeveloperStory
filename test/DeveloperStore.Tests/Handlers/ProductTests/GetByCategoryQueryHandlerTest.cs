using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;
using System.Net;
using NSubstitute.ExceptionExtensions;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class GetByCategoryQueryHandlerTest
    {
        private readonly ProductFixture _fixture;

        public GetByCategoryQueryHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Fact(DisplayName = "[GetByCategory] - Not Found")]
        [Trait("GetByCategory", "Not Found")]
        public async Task ItShould_GetByCategory_NotFound()
        {
            // Arrange
            var request = new GetByCategoryQueryRequestFaker().Generate();

            var response = new PaginatedResult<IEnumerable<Product>> { Data = [], TotalItems = 0 };

            _fixture._productRepository
                .GetByCategory(Arg.Any<string>(), Arg.Any<QueryOptions>())
                .Returns(response);

            // Act
            var result = await _fixture._GetByCategory.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Empty(result.Data);
        }

        [Fact(DisplayName = "[GetByCategory] - Success")]
        [Trait("GetByCategory", "Success")]
        public async Task ItShould_GetByCategory_Success()
        {
            // Arrange
            var products = new ProductFaker().Generate(5);

            var request = new GetByCategoryQueryRequest { Category = "Electronics", Page = 1, Size = 10, Order = "Name" };

            var response = new PaginatedResult<IEnumerable<Product>> { Data = products, TotalItems = 5 };

            _fixture._productRepository
                .GetByCategory(Arg.Any<string>(), Arg.Any<QueryOptions>())
                .Returns(response);

            // Act
            var result = await _fixture._GetByCategory.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);

            Assert.Equal(5, result.Data.Count());

            await _fixture._productRepository
                .Received(1)
                .GetByCategory(Arg.Any<string>(), Arg.Any<QueryOptions>());
        }

        [Fact(DisplayName = "[GetByCategory] - Exception")]
        [Trait("GetByCategory", "Exception")]
        public async Task ItShould_GetByCategory_Exception()
        {
            // Arrange
            var request = new GetByCategoryQueryRequest { Category = "Electronics", Page = 1, Size = 10, Order = "Name" };

            _fixture._productRepository
                .GetByCategory(Arg.Any<string>(), Arg.Any<QueryOptions>())
                .Throws(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture._GetByCategory.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
