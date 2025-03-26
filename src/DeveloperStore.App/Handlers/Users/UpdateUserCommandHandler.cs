using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Commands.User.Request;
using DeveloperStore.Domain.Services.Models;
using DeveloperStore.App.Models.Commands.User.Response;

namespace DeveloperStore.App.Handlers.Users
{
    public class UpdateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateUserCommandRequest, IResultResponse<UpdateUserCommandResponse>>
    {
        public async Task<IResultResponse<UpdateUserCommandResponse>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO Validation com FluentValidation
            var response = new ResultResponse<UpdateUserCommandResponse>();

            var entity = await userRepository.GetByIdAsync(request.UserId, new QueryOptions { IsAsNoTracking = true });

            if (entity == null || entity.UserId == 0)
            {
                response.AddMessage("User Not Found");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                return response;
            } 

            mapper.Map(request, entity);

            var result = await userRepository.UpdateAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data = mapper.Map<UpdateUserCommandResponse>(result);

            return response;
        }
    }
}
