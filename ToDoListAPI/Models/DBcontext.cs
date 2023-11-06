using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoListAPI.Models;

public class DBcontext:IdentityDbContext<IdentityUser>
{
    public DBcontext(DbContextOptions<DBcontext> options):base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection = "Data Source=.;Initial Catalog=DBcontext;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=True;";
        optionsBuilder.UseSqlServer(connection);
        // base.OnConfiguring(optionsBuilder);
    }
}
