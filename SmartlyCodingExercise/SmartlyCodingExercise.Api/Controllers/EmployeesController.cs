using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartlyCodingExercise.Api.Models.Dtos;
using SmartlyCodingExercise.Api.Repository.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartlyCodingExercise.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController(
        ILogger<EmployeesController> logger, 
        IMapper mapper, 
        IEmployeeRepository empRepo) : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IEmployeeRepository _empRepo = empRepo;

        [HttpGet]
        [SwaggerOperation(Summary = "Get Employees", OperationId = "GetEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _empRepo.GetAllAsyn();
                if (employees == null || employees.Count == 0)
                {
                    return NotFound();
                }
                var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                return Ok(employeesDto);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
