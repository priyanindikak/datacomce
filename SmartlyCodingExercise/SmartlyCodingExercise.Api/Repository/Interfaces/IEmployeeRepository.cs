using SmartlyCodingExercise.Api.Models;

namespace SmartlyCodingExercise.Api.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IList<Employee>> GetAllAsyn();
        Task<Employee?> GetByIdAsync(int employeeId);
    }
}
