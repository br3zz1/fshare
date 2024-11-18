using FShare.Domain;
using Microsoft.EntityFrameworkCore;

namespace FShare.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
