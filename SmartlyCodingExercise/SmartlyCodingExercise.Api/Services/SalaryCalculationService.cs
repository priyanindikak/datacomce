using SmartlyCodingExercise.Api.Common.Enums;
using SmartlyCodingExercise.Api.Models;
using SmartlyCodingExercise.Api.Models.Dtos;
using SmartlyCodingExercise.Api.Repository.Interfaces;

namespace SmartlyCodingExercise.Api.Services
{
    public class SalaryCalculationService(
        ILogger logger,
        IEmployeeRepository emplRepo,
        IEmployeeSalaryRepository empSalRepo,
        ISalaryHelperRepository salHelperRepo)
    {
        private readonly ILogger _logger = logger;
        private readonly IEmployeeRepository _emplRepo = emplRepo;
        private readonly IEmployeeSalaryRepository _empSalRepo = empSalRepo;
        private readonly ISalaryHelperRepository _salHelperRepo = salHelperRepo;

        public async Task<EmployeeSalaryDetailsDto> GetEmployeeSalaryDetailsByMonth(int empId, int year, Month month)
        {
            try
            {
                var employee = await _emplRepo.GetByIdAsync(empId);
                var employeeSalary = await _empSalRepo.GetByEmployeeAsync(empId);

                if (employee == null || employeeSalary == null)
                {
                    return new EmployeeSalaryDetailsDto();
                }

                var employeeSalaryDetails = await GetEmployeeSalaryCalculationDetails(employee.FirstName, 
                    employee.LastName, employeeSalary.AnnualSalary, employeeSalary.SuperRate, year, month);
                employeeSalaryDetails.Id = empId;
                return employeeSalaryDetails;
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<EmployeeSalaryDetailsDto> GetEmployeeSalaryCalculation(
            string firstName, string lastName, int annualSalary, int superRate, int year, Month month)
        {
            try
            {
                var employeeSalaryDetails = await GetEmployeeSalaryCalculationDetails(firstName, lastName, annualSalary, superRate, year, month);
                return employeeSalaryDetails;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        #region Private Methods

        private async Task<EmployeeSalaryDetailsDto> GetEmployeeSalaryCalculationDetails(
            string firstName, string lastName, int annualSalary, int superRate, int year, Month month)
        {
            try
            {
                var grossIncome = GetGrossIncome(annualSalary);
                var incomeTax = GetIncomeTax(annualSalary, await _salHelperRepo.GetTaxRatesAsync());

                var employeeSalaryDetails = new EmployeeSalaryDetailsDto
                {
                    Id = 0,
                    FullName = $"{firstName} {lastName}",
                    PayPeriod = GetPayPeriod(year, month),
                    GrossIncome = grossIncome,
                    IncomeTax = incomeTax,
                    NetIncome = GetNetIncome(grossIncome, incomeTax),
                    SuperAmount = GetSuper(grossIncome, superRate)
                };
                return employeeSalaryDetails;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        private static string GetPayPeriod(int year, Month month)
        {
            DateTime startDate = new DateTime(year, (int)month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            return $"{startDate:dd MMMM} - {endDate:dd MMMM}";
        }

        private static decimal GetGrossIncome(int annualSalary)
        {
            return (decimal)Math.Round((double)annualSalary / 12, 2);
        }

        private static decimal GetIncomeTax(int annualSalary, IList<TaxRate> taxRateList)
        {
            var incomeTax = 0m;
            var previousThreshold = 0;
            foreach (TaxRate taxRate in taxRateList.OrderBy(x => x.IncomeThreshold))
            {       
                var inCurrentSlabRange = annualSalary <= taxRate.IncomeThreshold;
                var taxableAmount = inCurrentSlabRange
                    ? annualSalary - previousThreshold 
                    : taxRate.IncomeThreshold - previousThreshold;
                incomeTax += taxableAmount * taxRate.Rate;
                if (inCurrentSlabRange)
                    break;
                previousThreshold = taxRate.IncomeThreshold;
            }
            return Math.Round(incomeTax / 12, 2);
        }

        private static decimal GetNetIncome(decimal grossIncome, decimal incomeTax)
        {
            return grossIncome - incomeTax;
        }

        private static decimal GetSuper(decimal grossIncome, int superRate)
        {
            return Math.Round(grossIncome * ((decimal)superRate / 100), 2);
        }

        #endregion
    }
}
