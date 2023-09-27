using FlyCheap.State.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyCheap.Db.Models;

public class UserStateContext : DbContext
{
    public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<Transfer> Transfer { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.ConnectionString);
    }
}