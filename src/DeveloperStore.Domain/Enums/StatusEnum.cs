using System.Runtime.Serialization;

namespace DeveloperStore.Domain.Enums
{
    public enum Status
    {
        [EnumMember(Value = "Active")]
        Active,

        [EnumMember(Value = "Inactive")]
        Inactive,

        [EnumMember(Value = "Suspended")]
        Suspended
    }
}
