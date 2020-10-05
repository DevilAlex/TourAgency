using Microsoft.EntityFrameworkCore;

namespace TourAgency.Models
{
    public class ManagerContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }//Manager - название таблицы БД
        public DbSet<Country> Countries { get; set; }
        public ManagerContext(DbContextOptions<ManagerContext> options)
            : base(options)
        { }

    }
}
