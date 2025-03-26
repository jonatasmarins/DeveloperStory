using DeveloperStore.Domain.Enums;

namespace DeveloperStore.App.Models.Queries.User.Response
{
    public class GetAllUserQueryResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public GetAllUserNameResponse Name { get; set; }
        public GetAllUserAddressResponse Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class GetAllUserNameResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class GetAllUserAddressResponse
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public GetAllUserGeolocationResponse GeoLocation { get; set; }
    }

    public class GetAllUserGeolocationResponse
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
