using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;
using System.Net;

namespace DeveloperStore.Tests.Handlers.ProductTests
{
    public class UpdateCommandHandlerTest
    {
        private readonly ProductFixture _fixture;

        public UpdateCommandHandlerTest()
        {
            _fixture = new ProductFixture();
        }

        [Fact(DisplayName = "[Update] - BadRequest")]
        [Trait("Update", "BadRequest")]
        public async Task ItShould_Update_BadRequest()
        {
            // Arrange
            var request = new UpdateProductCommandRequestFaker().Generate();

            request.Title = string.Empty;       

            // Act
            var result = await _fixture._Update.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            Assert.Contains($"{nameof(request.Title)} is Required", result.Erros);
        }

        [Theory(DisplayName = "[Update] - NotFound")]
        [Trait("Update", "NotFound")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ItShould_Update_NotFound(bool IsNull)
        {
            // Arrange
            var request = new UpdateProductCommandRequestFaker().Generate();                        

            _fixture._productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(_fixture.GetProductEmptyOrNull(IsNull));            

            // Act
            var result = await _fixture._Update.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Contains($"The Product with ID {request.Id} does not exist in our database", result.Erros);
        }

        [Fact(DisplayName = "[Update] - Success")]
        [Trait("Update", "Success")]
        public async Task ItShould_Update_Success()
        {
            // Arrange
            var request = new UpdateProductCommandRequestFaker().Generate();

            var product = new ProductFaker().Generate();

            var updateProduct = new ProductFaker().Generate();

            _fixture._productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(product);

            _fixture._productRepository
                .UpdateAsync(updateProduct)
                .Returns(updateProduct);

            _fixture._unitOfWork
                .SaveAsync(Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _fixture._Update.Handle(request, CancellationToken.None);

            // Assert
            await _fixture._productRepository
                .Received(1)
                .UpdateAsync(Arg.Is<Product>(p => p.Title == request.Title && p.Price == request.Price));

            await _fixture._unitOfWork.Received(1).SaveAsync(CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
