namespace SmartlyCodingExercise.Api.Models.Dtos
{
    public class EmployeeSalaryDetailsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PayPeriod { get; set; } = string.Empty;
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal SuperAmount { get; set; }
    }
}
