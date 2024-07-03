using SmartlyCodingExercise.Api.Models;

namespace SmartlyCodingExercise.Api.Repository.Interfaces
{
    public interface IEmployeeSalaryRepository
    {
        Task<EmployeeSalary?> GetByEmployeeAsync(int employeeId);
    }
}
