using Microsoft.EntityFrameworkCore;
using technical_challenge.Model;

namespace technical_challenge.DatabaseContext
{
    public class UserDatabaseContext : DbContext
    {
        public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; } = null!;
    }
}
