using System.Runtime.Serialization;

namespace DeveloperStore.Domain.Enums
{
    public enum Role
    {
        [EnumMember(Value = "Customer")]
        Customer = 1,

        [EnumMember(Value = "Manager")]
        Manager,

        [EnumMember(Value = "Admin")]
        Admin
    }
}
