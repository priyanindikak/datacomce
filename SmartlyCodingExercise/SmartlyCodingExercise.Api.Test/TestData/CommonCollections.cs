using SmartlyCodingExercise.Api.Models;

namespace SmartlyCodingExercise.Api.Test.TestData
{
    public class CommonCollections
    {
        public static IList<Employee> GetEmployees()
        {
            return
            [
                new(){ Id = 1, FirstName = "John", LastName="Smith" },
                new(){ Id = 2, FirstName = "Alex", LastName="Wong" }
            ];
        }

        public static Employee GetSingleEmployee()
        {
            return new Employee { Id = 1, FirstName = "John", LastName="Smith" };
        }

        public static EmployeeSalary GetSingleEmployeeSalary()
        {
            return new EmployeeSalary() { Id = 1, AnnualSalary = 60050, SuperRate = 9, EmployeeId = 1 };
        }

        public static IList<TaxRate> GetTaxRates()
        {
            return
            [
                new() { Id= 1, IncomeThreshold = 14000, Rate = 0.105m },
                new() { Id= 2, IncomeThreshold = 48000, Rate = 0.175m },
                new() { Id= 3, IncomeThreshold = 70000, Rate = 0.3m },
                new() { Id= 4, IncomeThreshold = 180000, Rate = 0.33m },
                new() { Id= 5, IncomeThreshold = 10000000, Rate = 0.39m },
            ];
        }
    }
}
