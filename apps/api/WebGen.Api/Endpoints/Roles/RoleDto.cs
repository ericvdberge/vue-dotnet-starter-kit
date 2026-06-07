namespace WebGen.Api.Endpoints.Roles;

public record RoleDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    //public required PermissionDto[] Permissions { get; set; }

}
