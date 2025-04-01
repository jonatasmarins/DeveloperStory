using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Commands.User.Request;
using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.App.Models;

namespace DeveloperStore.App.Handlers.Users
{
    public class DeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommandRequest, IResultResponse<DeleteUserCommandResponse>>
    {
        public async Task<IResultResponse<DeleteUserCommandResponse>> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<DeleteUserCommandResponse>() { Data = new DeleteUserCommandResponse() };

            var user = await userRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true, IsIgnoreAutoIncludes = true });

            if (user == null || user.Id == 0)
            {
                response.AddMessage("User not found!");

                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                return response;
            }

            await userRepository.DeleteAsync(request.Id);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data.Message = "User deleted with Success !";

            return response;
        }
    }
}
