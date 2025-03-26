using DeveloperStore.Domain.Enums;

namespace DeveloperStore.App.Models.Commands.User.Response
{
    public class UpdateUserCommandResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public UpdateUserNameResponse Name { get; set; }
        public UpdateUserAddressResponse Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class UpdateUserNameResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateUserAddressResponse
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public UpdateUserGeolocationResponse GeoLocation { get; set; }
    }

    public class UpdateUserGeolocationResponse
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
