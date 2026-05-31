namespace WebGen.Infrastructure.Dao;

public record UserDao
{
    public Guid Id { get; set; }
    public required string Name { get; init; }
    public required string Email { get; init; }
}
