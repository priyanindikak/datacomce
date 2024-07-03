using System.ComponentModel;

namespace SmartlyCodingExercise.Web.Models
{
    public class EmployeeSalaryDetails
    {
        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; } = string.Empty;
        [DisplayName("Pay Period")]
        public string PayPeriod { get; set; } = string.Empty;   
        
        public decimal GrossIncome { get; set; }
        [DisplayName("Gross Income")]
        public string GrossIncomeDisplay => $"{StringConstantsHelpers.Currency}{GrossIncome}";

        public decimal IncomeTax { get; set; }
        [DisplayName("Income Tax")]
        public string IncomeTaxDisplay => $"{StringConstantsHelpers.Currency}{IncomeTax}";

        public decimal NetIncome { get; set; }
        [DisplayName("Net Income")]
        public string NetIncomeDisplay => $"{StringConstantsHelpers.Currency}{NetIncome}";

        public decimal SuperAmount { get; set; }
        [DisplayName("Super")]
        public string SuperAmountDisplay => $"{StringConstantsHelpers.Currency}{SuperAmount}";
    }
}
