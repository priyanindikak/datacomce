using System.ComponentModel.DataAnnotations;

namespace SmartlyCodingExercise.Api.Models
{
    public class TaxRate
    {
        [Key]
        public int Id { get; set; }
        public int IncomeThreshold { get; set; }
        public decimal Rate { get; set; }
    }
}
