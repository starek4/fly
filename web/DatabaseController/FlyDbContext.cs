using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseController
{
    public class FlyDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConfigurationManager.AppSettings["ConnectionString"]);
        }
    }
}
