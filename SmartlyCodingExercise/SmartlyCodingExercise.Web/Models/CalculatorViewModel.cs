namespace SmartlyCodingExercise.Web.Models
{
    public class CalculatorViewModel
    {
        public bool IsCalculated { get; set; } = false;
        public CalculatorInputs CalcInputs { get; set; } = new CalculatorInputs();
        public EmployeeSalaryDetails SalaryDetailsSummary { get; set; } = new EmployeeSalaryDetails();
    }
}
