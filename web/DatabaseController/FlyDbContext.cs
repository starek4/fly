using System.Configuration;
using DatabaseController.Models;
using Microsoft.EntityFrameworkCore;

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
