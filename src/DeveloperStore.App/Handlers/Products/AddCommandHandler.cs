using AutoMapper;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class AddCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
    {
        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO Fluent Validator

            var entity = mapper.Map<Product>(request);

            var result = await productRepository.AddAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            return mapper.Map<AddProductCommandResponse>(result);
        }
    }
}
