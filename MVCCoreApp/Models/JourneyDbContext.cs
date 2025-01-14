using Microsoft.EntityFrameworkCore;

namespace MVCCoreApp.Models
{
    public class JourneyDbContext : DbContext
    {
        public JourneyDbContext(DbContextOptions<JourneyDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }  // The `Driver` class now maps to DriverDetails
        public DbSet<Login> Logins { get; internal set; }
        public DbSet<Schedules> Schedules { get; set; }
    }
}

