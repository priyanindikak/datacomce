using System.Text.Json.Serialization;

namespace SmartlyCodingExercise.Api.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string FirstName { get; set; } = string.Empty;
        [JsonIgnore]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
    }
}
