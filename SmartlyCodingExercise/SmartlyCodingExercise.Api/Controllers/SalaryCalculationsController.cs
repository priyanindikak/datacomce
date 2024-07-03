using Microsoft.AspNetCore.Mvc;
using SmartlyCodingExercise.Api.Common.Enums;
using SmartlyCodingExercise.Api.Repository.Interfaces;
using SmartlyCodingExercise.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartlyCodingExercise.Api.Controllers
{
    [Route("api/salarycalculations")]
    [ApiController]
    public class SalaryCalculationsController(
        ILogger<SalaryCalculationsController> logger,
        IEmployeeRepository empRepo,
        IEmployeeSalaryRepository empSalRepo,
        ISalaryHelperRepository salHelperRepo) : ControllerBase
    {
        private readonly ILogger<SalaryCalculationsController> _logger = logger;
        private readonly IEmployeeRepository _empRepo = empRepo;
        private readonly IEmployeeSalaryRepository _empSalRepo = empSalRepo;
        private readonly ISalaryHelperRepository _salHelperRepo = salHelperRepo;

        [HttpGet]
        [Route("{empId:int}/{year:int}/{month:int}")]
        [SwaggerOperation(Summary = "Get Salary Calculation By Employee", OperationId = "GetSalaryCalculationByEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] int empId, [FromRoute] int year, [FromRoute] int month)
        {
            try
            {
                if (empId == 0)
                {
                    _logger.LogInformation(StringConstantsHelpers.NoEmployeeFound);
                    return BadRequest(StringConstantsHelpers.NoEmployeeFound);
                }

                var salCalcService = new SalaryCalculationService(_logger, _empRepo, _empSalRepo, _salHelperRepo);
                var empSalDetails = await salCalcService.GetEmployeeSalaryDetailsByMonth(empId, year, (Month)month);
                if (empSalDetails == null ||
                    empSalDetails.Id == 0)
                {
                    return NotFound();
                }
                return Ok(empSalDetails);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("{fName}/{lName}/{aSal:int}/{sRate:int}/{year:int}/{month:int}")]
        [SwaggerOperation(Summary = "Get Salary Calculation Details", OperationId = "GetSalaryCalculationDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] string fName, [FromRoute] string lName, 
            [FromRoute] int aSal, [FromRoute] int sRate, [FromRoute] int year, [FromRoute] int month)
        {
            try
            {
                var salCalcService = new SalaryCalculationService(_logger, _empRepo, _empSalRepo, _salHelperRepo);
                var empSalDetails = await salCalcService.GetEmployeeSalaryCalculation(fName, lName, aSal, sRate, year, (Month)month);
                return Ok(empSalDetails);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
