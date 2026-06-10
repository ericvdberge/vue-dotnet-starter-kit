namespace WebGen.Domain.Entities;

public class Permission
{
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;

}
