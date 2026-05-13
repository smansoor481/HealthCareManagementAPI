using Microsoft.EntityFrameworkCore;
using UserService.Entity;

namespace UserService.Data
{
    public class UserDBContext : DbContext
    {

        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }


    }
}
