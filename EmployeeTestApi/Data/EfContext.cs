
using EmployeeTestApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeTestApi.Data
{
    public class EfContext: DbContext
    {
        public DbSet<EmployeeRm> Employee { get; set; }
        public DbSet<DepartmentRm> Department { get; set; }

        public EfContext(DbContextOptions<EfContext> options):
            base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeRm>().HasKey(u => u.EmployeeId);
            modelBuilder.Entity<EmployeeRm>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
            modelBuilder.Entity<DepartmentRm>().HasKey(u => u.DepartmentId);
        }
    }
}