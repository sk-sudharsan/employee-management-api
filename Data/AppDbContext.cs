using EmployeeManAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .UseIdentityByDefaultColumn();
        }


    }
}
