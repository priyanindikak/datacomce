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
        public string GrossIncomeDisplay => $"{StringConstantsHelpers.Currency}{string.Format("{0:0.00}", GrossIncome)}";

        public decimal IncomeTax { get; set; }
        [DisplayName("Income Tax")]
        public string IncomeTaxDisplay => $"{StringConstantsHelpers.Currency}{string.Format("{0:0.00}", IncomeTax)}";

        public decimal NetIncome { get; set; }
        [DisplayName("Net Income")]
        public string NetIncomeDisplay => $"{StringConstantsHelpers.Currency}{string.Format("{0:0.00}", NetIncome)}";

        public decimal SuperAmount { get; set; }
        [DisplayName("Super")]
        public string SuperAmountDisplay => $"{StringConstantsHelpers.Currency}{string.Format("{0:0.00}", SuperAmount)}";
    }
}
