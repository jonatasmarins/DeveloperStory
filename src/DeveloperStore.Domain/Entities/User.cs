namespace DeveloperStore.Domain.Entities
{
    public class Geolocation
    {        
        public string Lat { get; set; }        
        public string Long { get; set; }
    }

    public class Address
    {        
        public string City { get; set; }        
        public string Street { get; set; }        
        public int Number { get; set; }        
        public string Zipcode { get; set; }

        public Geolocation Geolocation { get; set; }
    }

    public class Name
    {        
        public string Firstname { get; set; }        
        public string Lastname { get; set; }
    }
}