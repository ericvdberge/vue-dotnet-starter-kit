namespace WebGen.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public Permission[]? Permissions { get; set; } = null;

}
