using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;
using System.Net;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class DeleteCommandHandlerTest
    {
        private readonly ProductFixture _fixture;

        public DeleteCommandHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Theory(DisplayName = "[Delete] - Not Found")]
        [Trait("Delete", "NotFound")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ItShould_Delete_NotFound(bool IsNull)
        {
            //Arrange
            var product = new ProductFaker().Generate();

            _fixture._productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(_fixture.GetProductEmptyOrNull(IsNull));

            var request = new DeleteProductCommandRequestFaker().Generate();

            //Act
            var result = await _fixture._Delete.Handle(request, CancellationToken.None);            

            //Assert
            Assert.False(result.Success);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact(DisplayName = "[Delete] - Success")]
        [Trait("Delete", "Success")]
        public async Task ItShould_Delete_Success()
        {
            //Arrange
            var product = new ProductFaker().Generate();

            _fixture._productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(product);

            var request = new DeleteProductCommandRequestFaker().Generate();

            //Act
            var result = await _fixture._Delete.Handle(request, CancellationToken.None);

            //Assert            
            await _fixture._productRepository
                .Received(1)
                .DeleteAsync(Arg.Any<int>());

            await _fixture._unitOfWork.Received(1).SaveAsync(CancellationToken.None);

            Assert.True(result.Success);
        }

    }
}
