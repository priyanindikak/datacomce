using Newtonsoft.Json;
using SmartlyCodingExercise.Web.Models;

namespace SmartlyCodingExercise.Web.Services
{
    public class SalaryInformationService : BaseService
    {
        public static async Task<EmployeeSalaryDetails?> GetByEmployee(string baseUrl, int empId, int year, int month)
        {
            try
            {
                var empSalDetails = new EmployeeSalaryDetails();
                var routeParams = $"/{empId}/{year}/{month}";
                await MakeHttpCall((HttpResponseMessage response) =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var empSalDetailsResponse = response.Content.ReadAsStringAsync().Result;
                        empSalDetails = JsonConvert.DeserializeObject<EmployeeSalaryDetails>(empSalDetailsResponse);
                    }
                },
                baseUrl, StringConstantsHelpers.EndpointSalaryCalculations, routeParams, StringConstantsHelpers.MediaTypeJson);
                return empSalDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public static async Task<EmployeeSalaryDetails> 
            GetSalaryCalculation(string baseUrl, string fName, string lName, int aSal, int sRate, int year, int month)
        {
            try
            {
                var empSalDetails = new EmployeeSalaryDetails();
                var routeParams = $"/{fName}/{lName}/{aSal}/{sRate}/{year}/{month}";
                await MakeHttpCall((HttpResponseMessage response) =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var empSalDetailsResponse = response.Content.ReadAsStringAsync().Result;
                        empSalDetails = JsonConvert.DeserializeObject<EmployeeSalaryDetails>(empSalDetailsResponse);
                    }
                },
                baseUrl, StringConstantsHelpers.EndpointSalaryCalculations, routeParams, StringConstantsHelpers.MediaTypeJson);
                return empSalDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
