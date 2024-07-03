using Microsoft.EntityFrameworkCore;
using SmartlyCodingExercise.Api.Models;

namespace SmartlyCodingExercise.Api.Data
{
    public class SCEDbContext : DbContext
    {
        public SCEDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalary> EmployeesSalaries { get; set;}
        public DbSet<TaxRate> TaxRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().HasData(IMData.GetEmployees());
            builder.Entity<EmployeeSalary>().HasData(IMData.GetEmployeeSalaries());
            builder.Entity<TaxRate>().HasData(IMData.GetTaxRates());
        }
    }
}
