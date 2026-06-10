namespace WebGen.Api.Endpoints.Roles;

public record RoleDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public PermissionDto[]? Permissions { get; set; } = null;

}
