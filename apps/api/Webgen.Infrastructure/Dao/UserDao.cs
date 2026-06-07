namespace WebGen.Infrastructure.Dao;

public record UserDao: BaseDao
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public bool Active { get; set; } = false;
}
