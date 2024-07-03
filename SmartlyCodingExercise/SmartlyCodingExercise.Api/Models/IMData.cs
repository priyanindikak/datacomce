namespace SmartlyCodingExercise.Api.Models
{
    public class IMData
    {
        public static IList<Employee> GetEmployees()
        {
            return
            [
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith"
                },
                new()
                {
                    Id = 2,
                    FirstName = "Alex",
                    LastName = "Wong"
                }
            ];
        }
        public static IList<EmployeeSalary> GetEmployeeSalaries()
        {
            return
            [
                new()
                {
                    Id = 1,
                    AnnualSalary = 60050,
                    SuperRate = 9,
                    EmployeeId = 1
                },
                new()
                {
                    Id = 2,
                    AnnualSalary = 120000,
                    SuperRate = 10,
                    EmployeeId = 2
                }
            ];
        }
        public static IList<TaxRate> GetTaxRates()
        {
            return
            [
                new TaxRate()
                {
                    Id= 1,
                    IncomeThreshold = 14000,
                    Rate = 0.105m
                },
                new TaxRate()
                {
                    Id= 2,
                    IncomeThreshold = 48000,
                    Rate = 0.175m
                },
                new TaxRate()
                {
                    Id= 3,
                    IncomeThreshold = 70000,
                    Rate = 0.3m
                },
                new TaxRate()
                {
                    Id= 4,
                    IncomeThreshold = 180000,
                    Rate = 0.33m
                },
                new TaxRate()
                {
                    Id= 5,
                    IncomeThreshold = 10000000,
                    Rate = 0.39m
                },
            ];
        }
    }
}
