using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeveloperStore.Infra.Context.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {   
        public int UserId { get; set; }
        public Name Name { get; set; } = new Name();

        public Address Address { get; set; } = new Address();

        public string Phone { get; set; } = string.Empty;

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }        
    }    
}
