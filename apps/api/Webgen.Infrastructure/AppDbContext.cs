using Microsoft.EntityFrameworkCore;
using WebGen.Infrastructure.Dao;

namespace Webgen.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    /// <summary>
    /// RBAC (Role Based Access Control)
    /// </summary>
    public DbSet<UserDao> Users { get; set; }
    public DbSet<RoleDao> Roles { get; set; }
    //public DbSet<Permission> Permissions { get; set; }
}
