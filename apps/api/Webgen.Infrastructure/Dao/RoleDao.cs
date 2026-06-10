namespace WebGen.Infrastructure.Dao;

public sealed record RoleDao: BaseDao
{
    public required string Name { get; init; }
    public IReadOnlyList<PermissionDao>? Permissions { get; init; }
}
