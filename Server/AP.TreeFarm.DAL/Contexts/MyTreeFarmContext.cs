using System.Reflection;
using AP.MyTreeFarm.Domain;
using AP.MyTreeFarm.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace AP.MyTreeFarm.Infrastructure.Contexts
{
    public class MyTreeFarmContext : DbContext
    {
        public MyTreeFarmContext(DbContextOptions<MyTreeFarmContext> options) : base(options)
        {

        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeTask> TreeTasks { get; set; }
        public DbSet<Zone> Zones { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Employee>().Seed();
            modelBuilder.Entity<Site>().Seed();
            modelBuilder.Entity<Tree>().Seed();
            modelBuilder.Entity<TreeTask>().Seed();
            modelBuilder.Entity<Zone>().Seed();
            
        }
    }
}
