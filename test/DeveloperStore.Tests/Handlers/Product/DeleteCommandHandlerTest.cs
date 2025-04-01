using Bogus;
using DeveloperStore.App.Handlers.Products;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;
using System.Net;

namespace DeveloperStore.Tests.Handlers.Product
{
    public class DeleteCommandHandlerTest
    {
        private IProductRepository productRepository;
        private IUnitOfWork unitOfWork;        

        public DeleteCommandHandlerTest()
        {
            productRepository = Substitute.For<IProductRepository>();            
            unitOfWork = Substitute.For<IUnitOfWork>();
        }

        [Fact]
        public async Task ItShould_Delete_NotFound()
        {
            //Arrange
            var obj = new Domain.Entities.Product();

            productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(obj);

            var request = new Faker<DeleteProductCommandRequest>().RuleFor(x => x.Id, f => f.Random.Int(1, 5));

            //Act
            var handler = new DeleteCommandHandler(productRepository, unitOfWork);

            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(response.Success);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ItShould_Delete_Success()
        {
            //Arrange
            var obj = new Domain.Entities.Product() { Id = 10 };

            productRepository
                .GetByIdAsync(Arg.Any<int>(), Arg.Any<QueryOptions>())
                .Returns(obj);

            var request = new Faker<DeleteProductCommandRequest>().RuleFor(x => x.Id, f => f.Random.Int(1, 5));

            //Act
            var handler = new DeleteCommandHandler(productRepository, unitOfWork);

            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(response.Success);            
        }

    }
}
