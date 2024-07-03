using Microsoft.AspNetCore.Mvc.Rendering;
using SmartlyCodingExercise.Web.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartlyCodingExercise.Web.Models
{
    public class CalculatorInputs
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue)]
        [DisplayName("Annual Salary")]
        public int AnnualSalary { get; set; }
        [Required]
        [Range(0, 50)]
        [DisplayName("Super Rate")]
        public int SuperRate { get; set; }
        [Required]
        [DisplayName("Pay Period")]
        [Range(1, 12)]
        public int PayPeriod { get; set; }

        public IList<SelectListItem> MonthList { get; set; } = DropDownCollections.GetMonthsList();
    }
}
