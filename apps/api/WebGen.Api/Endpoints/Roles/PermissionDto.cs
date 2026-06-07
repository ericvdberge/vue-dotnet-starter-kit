namespace WebGen.Api.Endpoints.Roles;

public record PermissionDto
{
    public Guid Id { get; set; }
    public required string Resource { get; set; }
    public required string Action { get; set; }
}
