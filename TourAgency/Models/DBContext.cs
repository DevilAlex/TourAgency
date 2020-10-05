using Microsoft.EntityFrameworkCore;

namespace TourAgency.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }//Manager - название таблицы БД
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<RestType> RestTypes { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourOperator> TourOperators { get; set; }
        public DbSet<Transport> Transports { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        { }

    }
}
