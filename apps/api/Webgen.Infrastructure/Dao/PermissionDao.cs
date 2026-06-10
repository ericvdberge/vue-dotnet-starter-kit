namespace WebGen.Infrastructure.Dao;

public record PermissionDao: BaseDao
{
    public Guid RoleId { get; init; }
    public required string Source { get; init; }
    public required string Action { get; init; }
}
