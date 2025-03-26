using AutoMapper;
using DeveloperStore.App.Models.Queries.User.Request;
using DeveloperStore.App.Models.Queries.User.Response;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Users
{
    public class GetAllUserQueryHandler(
        IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<GetAllUserQueryRequest, IResultResponse<IEnumerable<GetAllUserQueryResponse>>>
    {
        public async Task<IResultResponse<IEnumerable<GetAllUserQueryResponse>>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                Page = request.Page,
                Size = request.Size,
                Order = request.Order,
            };

            var result = await userRepository.GetAllAsync(options);

            return mapper.Map<ResultResponse<IEnumerable<GetAllUserQueryResponse>>>(result);
        }
    }
}
