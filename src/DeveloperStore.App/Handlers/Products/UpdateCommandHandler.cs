using AutoMapper;
using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class UpdateCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<Product> validator,
        IMapper mapper) : IRequestHandler<UpdateProductCommandRequest, IResultResponse<UpdateProductCommandResponse>>
    {
        public async Task<IResultResponse<UpdateProductCommandResponse>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<UpdateProductCommandResponse>();

            var validatorResult = IsValid(request);

            if (!validatorResult.IsValid)
            {
                response.AddMessage(validatorResult.Errors);

                response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                return response;
            }

            var entity = await productRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true });

            if (entity == null || entity.Id == 0)
            {
                response.AddMessage($"The Product with ID {request.Id} does not exist in our database");

                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                return response;
            }

            mapper.Map(request, entity);

            var result = await productRepository.UpdateAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data = mapper.Map<UpdateProductCommandResponse>(result);

            return response;
        }

        private ValidationResult IsValid(UpdateProductCommandRequest request)
        {
            var entity = mapper.Map<Product>(request);

            return validator.Validate(entity);
        }
    }
}
