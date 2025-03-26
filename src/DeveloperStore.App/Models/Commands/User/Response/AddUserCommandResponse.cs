using DeveloperStore.Domain.Enums;

namespace DeveloperStore.App.Models.Commands.User.Response
{
    public class AddUserCommandResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AddUserNameResponse Name { get; set; }
        public AddUserAddressResponse Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class AddUserNameResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AddUserAddressResponse
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public AddUserGeolocationResponse GeoLocation { get; set; }
    }

    public class AddUserGeolocationResponse
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
