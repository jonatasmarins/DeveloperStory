using DeveloperStore.Domain.Enums;

namespace DeveloperStore.App.Models.Queries.User.Response
{
    public class GetByIdUserQueryResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public GetByIdUserNameResponse Name { get; set; }
        public GetByIdUserAddressResponse Address { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }

    public class GetByIdUserNameResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class GetByIdUserAddressResponse
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public GetByIdUserGeolocationResponse GeoLocation { get; set; }
    }

    public class GetByIdUserGeolocationResponse
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
