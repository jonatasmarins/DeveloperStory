using AutoMapper;
using DeveloperStore.App.Models.Commands.User.Request;
using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Services.Models;
using DeveloperStore.Infra.Context.Identity;
using MediatR;

namespace DeveloperStore.App.Handlers.Users
{
    public class AddUserCommandHandler(
        IUserRepository userRepository,
        IMapper mapper
    ) : IRequestHandler<AddUserCommandRequest, IResultResponse<AddUserCommandResponse>>
    {
        public async Task<IResultResponse<AddUserCommandResponse>> Handle(AddUserCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO FLuent Validation
            var result = new ResultResponse<AddUserCommandResponse>();

            var user = mapper.Map<ApplicationUser>(request);
        
            var userCreate = await userRepository.AddAsync(user, request.Password);

            if (userCreate.Succeeded)
            {
                await userRepository.AddToRoleAsync(user, user.Role);
            }
            else
            {
                foreach (var item in userCreate.Errors)
                {
                    result.AddMessage($"{item.Code} - {item.Description}");
                }
            }

            result.Data = mapper.Map<AddUserCommandResponse>(user);

            return result;
        }
    }
}
