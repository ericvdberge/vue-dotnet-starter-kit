namespace WebGen.Domain.Entities;

public record User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];

    public void AssignRole(Guid roleId)
    {
        var userRole = new UserRole
        {
            UserId = Id,
            RoleId = roleId
        };

        UserRoles.Add(userRole);
    }
}
