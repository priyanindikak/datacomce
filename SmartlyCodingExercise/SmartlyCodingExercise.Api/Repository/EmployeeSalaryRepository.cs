using Microsoft.EntityFrameworkCore;
using SmartlyCodingExercise.Api.Data;
using SmartlyCodingExercise.Api.Models;
using SmartlyCodingExercise.Api.Repository.Interfaces;

namespace SmartlyCodingExercise.Api.Repository
{
    public class EmployeeSalaryRepository(
        ILogger<EmployeeSalaryRepository> logger, 
        SCEDbContext context) : IEmployeeSalaryRepository
    {
        private readonly ILogger<EmployeeSalaryRepository> _logger = logger;
        private readonly SCEDbContext _context = context;

        public async Task<EmployeeSalary?> GetByEmployeeAsync(int empId)
        {
            try
            {
                var empSalaryList = CommonConstantsHelpers.InMemoryDataMode
                    ? IMData.GetEmployeeSalaries()
                    : await _context.EmployeesSalaries.ToListAsync();
                var empSalary = empSalaryList.Where(x => x.EmployeeId == empId).FirstOrDefault();
                return empSalary;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
