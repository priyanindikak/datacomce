using Microsoft.AspNetCore.Mvc;
using SmartlyCodingExercise.Web.Models;
using SmartlyCodingExercise.Web.Services;

namespace SmartlyCodingExercise.Web.Controllers
{
    public class SalaryInformationController(IConfiguration configuration) : BaseController(configuration)
    {
        public async Task<IActionResult> Index()
        {
            try
            {                
                var empList = await EmployeeService.GetAll(ApiUrl);
                return View(empList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction(StringConstantsHelpers.CommonErrorView);
            }
        }

        public async Task<IActionResult> ViewInfo(int empId)
        {
            try
            {
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var empSalInfo = await SalaryInformationService.GetByEmployee(ApiUrl, empId, year, month);
                TempData[StringConstantsHelpers.PaySlipHeader] = StringConstantsHelpers.TextEmployeePaySlip;
                TempData[StringConstantsHelpers.TempDataYear] = year;
                return View(empSalInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction(StringConstantsHelpers.CommonErrorView);
            }
        }

        public IActionResult Calculator()
        {
            var calculatorDto = new CalculatorViewModel();
            return View(calculatorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Calculator(CalculatorViewModel calcDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(calcDto);
                }

                var year = DateTime.Now.Year;
                var month = calcDto.CalcInputs.PayPeriod;
                TempData[StringConstantsHelpers.PaySlipHeader] = StringConstantsHelpers.TextCalcPaySlipSummary;
                TempData[StringConstantsHelpers.TempDataYear] = year;

                var inputs = calcDto.CalcInputs;
                var salaryDetailsSummary = await SalaryInformationService.GetSalaryCalculation(ApiUrl, 
                    inputs.FirstName, inputs.LastName, inputs.AnnualSalary, inputs.SuperRate, year, month);

                var viewModel = new CalculatorViewModel
                {
                    IsCalculated = true,
                    CalcInputs = inputs,
                    SalaryDetailsSummary = salaryDetailsSummary
                };         
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction(StringConstantsHelpers.CommonErrorView);
            }
        }
    }
}
