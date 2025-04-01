using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.Domain.Enums;
using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Models.Commands.User.Request
{
    public class AddUserCommandRequest : IRequest<IResultResponse<AddUserCommandResponse>>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AddUserNameRequest Name { get; set; }
        public AddUserAddressRequest Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class AddUserNameRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AddUserAddressRequest
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public AddUserGeolocationRequest GeoLocation { get; set; }
    }

    public class AddUserGeolocationRequest
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
