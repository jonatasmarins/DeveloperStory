using DeveloperStore.App.Models.Commands.Auth;
using DeveloperStore.Domain.Services.Models;
using DeveloperStore.Infra.Context.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeveloperStore.App.Handlers.Auth
{
    public class RegisterCommandHandler(
        UserManager<ApplicationUser> userManager        
    ) : IRequestHandler<RegisterCommandRequest, IResultResponse<RegisterCommandResponse>>
    {
        public async Task<IResultResponse<RegisterCommandResponse>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO FLuent Validation
            var result = new ResultResponse<RegisterCommandResponse>();

            var user = new ApplicationUser
            {
                UserName = request.UserName,
            };

            var userCreate = await userManager.CreateAsync(user, request.Password);

            if (!userCreate.Succeeded)
            {
                foreach (var item in userCreate.Errors)
                {
                    result.AddMessage($"{item.Code} - {item.Description}");
                }
            }

            return result;
        }
    }
}
