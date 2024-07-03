using Microsoft.AspNetCore.Mvc;
using SmartlyCodingExercise.Web.Models;
using System.Diagnostics;

namespace SmartlyCodingExercise.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiUrl => _configuration[StringConstantsHelpers.AppSettingsApiUrl] ?? string.Empty;


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
