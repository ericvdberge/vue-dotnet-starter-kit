using Microsoft.EntityFrameworkCore;
using WebGen.Domain.Entities;

namespace Webgen.Infrastructure;

public class AppDbContext: DbContext
{

    /// <summary>
    /// RBAC (Role Based Access Control)
    /// </summary>
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
}
