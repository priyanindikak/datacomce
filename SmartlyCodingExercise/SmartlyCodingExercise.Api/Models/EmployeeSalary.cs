using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartlyCodingExercise.Api.Models
{
    public class EmployeeSalary
    {
        [Key]
        public int Id { get; set; }
        public int AnnualSalary { get; set; }
        public int SuperRate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }        
        public Employee? Employee { get; set; }
    }
}
