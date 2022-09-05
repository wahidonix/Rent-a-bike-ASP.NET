using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Station> Stations { get; set; }
    }
}
