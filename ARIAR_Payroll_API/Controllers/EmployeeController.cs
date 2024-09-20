using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll_Library.Models.Dto;
using Payroll_Library.Services;
using Payroll_Library.Services.Employee;

namespace ARIAR_Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost("InsertOrUpdateEmployeeInfo")]
        public async Task<ActionResult<ApiResponse<string>>> InsertOrUpdateEmployeeInfo(PersonalInformationDto dto)
        {
            var result = await _employeeService.AddEmployeeInfo(dto);
            return Ok(result);

        }
         [HttpPost("AddPosition")]
        public async Task<ActionResult<ApiResponse<string>>> AddPosition(PostionDto dto)
        {
            var result = await _employeeService.AddPosition(dto);
            return Ok(result);

        }

    }
}
