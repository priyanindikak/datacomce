using Newtonsoft.Json;
using SmartlyCodingExercise.Web.Models;

namespace SmartlyCodingExercise.Web.Services
{
    public class EmployeeService : BaseService
    {
        public static async Task<List<Employee>?> GetAll(string baseUrl)
        {
            var employees = new List<Employee>();
            await MakeHttpCall((HttpResponseMessage response) =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var empResponse = response.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<Employee>>(empResponse);
                }
            }, 
            baseUrl, StringConstantsHelpers.EndpointEmployees, string.Empty, StringConstantsHelpers.MediaTypeJson);
            return employees;
        }

    }
}
