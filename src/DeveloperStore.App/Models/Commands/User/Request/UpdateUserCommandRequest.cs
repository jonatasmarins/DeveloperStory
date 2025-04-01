using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.Domain.Enums;
using DeveloperStore.App.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.User.Request
{    
    public class UpdateUserCommandRequest : IRequest<IResultResponse<UpdateUserCommandResponse>>
    {
        [JsonIgnore]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UpdateUserNameRequest Name { get; set; }
        public UpdateUserAddressRequest Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class UpdateUserNameRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateUserAddressRequest
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public UpdateUserGeolocationRequest GeoLocation { get; set; }
    }

    public class UpdateUserGeolocationRequest
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
