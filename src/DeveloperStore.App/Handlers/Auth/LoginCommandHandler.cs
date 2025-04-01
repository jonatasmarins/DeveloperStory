using DeveloperStore.App.Models.Commands.Auth;
using DeveloperStore.App.Models;
using DeveloperStore.Infra.Context.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace DeveloperStore.App.Handlers.Auth
{
    public class LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signIn
    ) : IRequestHandler<LoginCommandRequest, IResultResponse<LoginCommandResponse>>
    {
        public async Task<IResultResponse<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var result = new ResultResponse<LoginCommandResponse>(new LoginCommandResponse());

            var user = await userManager.FindByNameAsync(request.UserName);            

            if (user == null)
            {
                AddMessageErro(ref result, HttpStatusCode.Unauthorized);

                return result;
            }

            var userSignin = await signIn.PasswordSignInAsync(user, request.Password, false, false);

            if (user == null || !userSignin.Succeeded)
            {
                AddMessageErro(ref result, HttpStatusCode.Unauthorized);

                return result;
            }

            result.Data.Token = GetToken();

            return result;
        }

        private static void AddMessageErro(ref ResultResponse<LoginCommandResponse> result, HttpStatusCode statusCode)
        {
            result.AddMessage("Invalid credentials");

            result.StatusCode = statusCode;
        }

        private static string GetToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(IdentitySettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
