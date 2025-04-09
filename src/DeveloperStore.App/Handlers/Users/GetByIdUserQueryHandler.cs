using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Queries.User.Request;
using DeveloperStore.App.Models.Queries.User.Response;
using DeveloperStore.App.Models;

namespace DeveloperStore.App.Handlers.Users
{
    public class GetByIdUserQueryHandler(
        IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<GetByIdUserQueryRequest, IResultResponse<GetByIdUserQueryResponse>>
    {
        public async Task<IResultResponse<GetByIdUserQueryResponse>> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<GetByIdUserQueryResponse>();

            var options = new QueryOptions
            {
                IsAsNoTracking = true
            };

            var result = await userRepository.GetByIdAsync(request.Id, options);

            if (result is null || result.Id == 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.AddMessage($"User with {request.Id} ID does not exist in our database");
            }

            response.Data = mapper.Map<GetByIdUserQueryResponse>(result);

            return response;
        }
    }
}
