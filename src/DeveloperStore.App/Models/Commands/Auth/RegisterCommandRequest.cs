using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Models.Commands.Auth
{
    public class RegisterCommandRequest : IRequest<IResultResponse<RegisterCommandResponse>>
    {        
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
