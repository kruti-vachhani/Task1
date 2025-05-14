using Microsoft.EntityFrameworkCore;

namespace UserManagement.Models;

public class UserManagementDbContext : DbContext
{

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
       : base(options)
    {
    }
    
    public DbSet<Persons> Persons { get; set; }
}
