using AutoMapper;
using Bogus;
using DeveloperStore.App.Handlers.Products;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using NSubstitute;

namespace DeveloperStore.Tests.Handlers.Product
{
    public class DeleteCommandHandlerTest
    {
        private IProductRepository productRepository;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public DeleteCommandHandlerTest()
        {
            productRepository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
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
            var handler = new DeleteCommandHandler(productRepository, unitOfWork, mapper);

            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(response.IsSuccess);
            Assert.Contains("Produt not found!", response.Message);
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
            var handler = new DeleteCommandHandler(productRepository, unitOfWork, mapper);

            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(response.IsSuccess);
            Assert.Contains("Product deleted with Success !", response.Message);
        }

    }
}
