using Microsoft.EntityFrameworkCore;
using SmartlyCodingExercise.Api.Data;
using SmartlyCodingExercise.Api.Models;
using SmartlyCodingExercise.Api.Repository.Interfaces;

namespace SmartlyCodingExercise.Api.Repository
{
    public class EmployeeRepository(
        ILogger<EmployeeRepository> logger, 
        SCEDbContext context) : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> _logger = logger;
        private readonly SCEDbContext _context = context;

        public async Task<IList<Employee>> GetAllAsyn()
        {
            try
            {
                return CommonConstantsHelpers.InMemoryDataMode
                    ? IMData.GetEmployees()
                    : await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            try
            {
                var employeeList = CommonConstantsHelpers.InMemoryDataMode
                    ? IMData.GetEmployees()
                    : await _context.Employees.ToListAsync();
                var employee = employeeList.Where(x => x.Id == employeeId).FirstOrDefault();
                return employee;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}