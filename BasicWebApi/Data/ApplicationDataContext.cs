using BasicWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BasicWebApi.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
