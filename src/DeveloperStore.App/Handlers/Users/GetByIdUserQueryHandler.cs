using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Queries.User.Request;
using DeveloperStore.App.Models.Queries.User.Response;

namespace DeveloperStore.App.Handlers.Users
{
    public class GetByIdUserQueryHandler(
        IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<GetByIdUserQueryRequest, GetByIdUserQueryResponse>
    {
        public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
        {
            var options = new QueryOptions
            {
                IsAsNoTracking = true
            };

            var result = await userRepository.GetByIdAsync(request.Id, options);

            return mapper.Map<GetByIdUserQueryResponse>(result);
        }
    }
}
