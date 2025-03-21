using AutoMapper;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class UpdateCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateCommandRequest, UpdateCommandResponse>
    {
        public async Task<UpdateCommandResponse> Handle(UpdateCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO Validation com FluentValidation

            var entity = await productRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true });

            if (entity == null || entity.Id == 0) return new UpdateCommandResponse();

            mapper.Map(request, entity);

            var result = await productRepository.UpdateAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            return mapper.Map<UpdateCommandResponse>(result);
        }
    }
}
