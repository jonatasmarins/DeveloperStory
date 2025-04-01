using AutoMapper;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.App.Models;
using FluentValidation;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class AddCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<Product> validator,
        IMapper mapper) : IRequestHandler<AddProductCommandRequest, IResultResponse<AddProductCommandResponse>>
    {
        public async Task<IResultResponse<AddProductCommandResponse>> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<AddProductCommandResponse>();

            var entity = mapper.Map<Product>(request);

            var validatorResult = validator.Validate(entity);

            if (!validatorResult.IsValid)
            {
                response.AddMessage(validatorResult.Errors);

                response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                return response;
            }

            var result = await productRepository.AddAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data = mapper.Map<AddProductCommandResponse>(result);

            return response;
        }
    }
}
