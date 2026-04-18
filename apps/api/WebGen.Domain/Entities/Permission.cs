namespace WebGen.Domain.Entities;

public class Permission
{
    public Guid Id { get; set; }

    public required string Action { get; set; }

    public required string Resource { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
