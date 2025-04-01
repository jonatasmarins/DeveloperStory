using AutoMapper;
using DeveloperStore.App.Models.Commands.User.Request;
using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.App.Models.Queries.User.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.App.Models;
using DeveloperStore.Infra.Context.Identity;

namespace DeveloperStore.App.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(ResultResponse<>))
                .ReverseMap();

            #region Add

            CreateMap<AddUserCommandRequest, ApplicationUser>()
                .ReverseMap();

            CreateMap<AddUserNameRequest, Name>()
                .ReverseMap();

            CreateMap<AddUserAddressRequest, Address>()
                .ReverseMap();

            CreateMap<AddUserGeolocationRequest, Geolocation>()
                .ReverseMap();

            CreateMap<ApplicationUser, AddUserCommandResponse>()                
                .ReverseMap();

            CreateMap<AddUserNameResponse, Name>()
                .ReverseMap();

            CreateMap<AddUserAddressResponse, Address>()
                .ReverseMap();

            CreateMap<AddUserGeolocationResponse, Geolocation>()
                .ReverseMap();

            #endregion

            #region GetAll

            CreateMap<GetAllUserNameResponse, Name>()
                .ReverseMap();

            CreateMap<GetAllUserAddressResponse, Address>()
                .ReverseMap();

            CreateMap<GetAllUserGeolocationResponse, Geolocation>()
                .ReverseMap();

            CreateMap<GetAllUserQueryResponse, ApplicationUser>()                            
                .ReverseMap();

            #endregion

            #region Update

            CreateMap<UpdateUserCommandRequest, ApplicationUser>()                           
                .ReverseMap();

            CreateMap<UpdateUserNameRequest, Name>()
                .ReverseMap();

            CreateMap<UpdateUserAddressRequest, Address>()
                .ReverseMap();

            CreateMap<UpdateUserGeolocationRequest, Geolocation>()
                .ReverseMap();

            CreateMap<ApplicationUser, UpdateUserCommandResponse>()                
                .ReverseMap();

            CreateMap<UpdateUserNameResponse, Name>()
                .ReverseMap();

            CreateMap<UpdateUserAddressResponse, Address>()
                .ReverseMap();

            CreateMap<UpdateUserGeolocationResponse, Geolocation>()
                .ReverseMap();

            #endregion

        }
    }
}
