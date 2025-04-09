using DeveloperStore.App.Models.Queries.Product.Requests;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class GetAllCategoriesQueryHandlerTest
    {
        private readonly ProductFixture _fixture;

        public GetAllCategoriesQueryHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Theory(DisplayName = "[GetAllCategories] - Not Found")]
        [Trait("GetAllCategories", "NotFound")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ItShould_GetAllCategories_NotFound(bool IsNull)
        {
            // Arrange
            var request = new GetAllCategoriesQueryRequest();

            _fixture._productRepository
                .GetAllCategories()
                .Returns(_fixture.GetCategoriesEmptyOrNull(IsNull));

            // Act
            var result = await _fixture._GetAllCategories.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.Success);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact(DisplayName = "[GetAllCategories] - Success")]
        [Trait("GetAllCategories", "Success")]
        public async Task ItShould_GetAllCategories_Success()
        {
            // Arrange
            var request = new GetAllCategoriesQueryRequest();

            _fixture._productRepository
                .GetAllCategories()
                .Returns(_fixture.GetCategories());

            // Act
            var result = await _fixture._GetAllCategories.Handle(request, CancellationToken.None);

            //Assert            

            await _fixture._productRepository
                .Received(1)
                .GetAllCategories();            

            Assert.True(result.Success);
        }

        [Fact(DisplayName = "[GetAllCategories] - Exception")]
        [Trait("GetAllCategories", "Exception")]
        public async Task ItShould_GetAllCategories_Exception()
        {
            // Arrange
            _fixture._productRepository.GetAllCategories().Throws(new Exception("Database error"));

            var request = new GetAllCategoriesQueryRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _fixture._GetAllCategories.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
