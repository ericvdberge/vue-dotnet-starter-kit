namespace WebGen.Domain.Entities;

public record Role
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    //public ICollection<RolePermission> RolePermissions { get; set; } = [];
    //public ICollection<UserRole> UserRoles { get; set; } = [];

}
