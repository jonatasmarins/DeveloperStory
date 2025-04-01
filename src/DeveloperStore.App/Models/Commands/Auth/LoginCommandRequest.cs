using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Models.Commands.Auth
{
    public class LoginCommandRequest : IRequest<IResultResponse<LoginCommandResponse>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
