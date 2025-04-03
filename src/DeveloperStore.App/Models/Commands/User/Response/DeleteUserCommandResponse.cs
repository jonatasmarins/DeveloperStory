using DeveloperStore.Domain.Enums;

namespace DeveloperStore.App.Models.Commands.User.Response
{
    public class DeleteUserCommandResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }        
        public DeleteUserNameResponse Name { get; set; }
        public DeleteUserAddressResponse Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class DeleteUserNameResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class DeleteUserAddressResponse
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public DeleteUserGeolocationResponse GeoLocation { get; set; }
    }

    public class DeleteUserGeolocationResponse
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
