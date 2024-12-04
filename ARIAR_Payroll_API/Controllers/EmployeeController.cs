using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.EmployeeDto;
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


        [HttpPost("AddOrUpdateEmployeeInfo")]
        public async Task<ActionResult<ApiResponse<string>>> AddOrUpdateEmployeeInfo(PersonalInformationDto dto)
        {
            var result = await _employeeService.AddOrUpdateEmployeeInfo(dto);
            return Ok(result);

        }
        [HttpPost("AddPosition")]
        public async Task<ActionResult<ApiResponse<string>>> AddPosition(PostionDto dto)
        {
            var result = await _employeeService.AddPosition(dto);
            return Ok(result);

        }
        [HttpDelete("DeleteEmployee")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteEmployee(DeleteEmployeeDto dto)
        {
            var result = await _employeeService.DeleteEmployee(dto);
            return Ok(result);
        }

        [HttpGet("DisplayPersonalInfo")]
        public async Task<ActionResult<ApiResponse<List<PersonalInformationDisplayDto>>>> DisplayPersonalInfo()
        {
            var result = await _employeeService.DisplayPersonalInfo();
            return Ok(result);
        }

        [HttpGet("DisplayPersonalInfoById/{id}")]
        public async Task<ActionResult<ApiResponse<PersonalInformationDisplayDto>>> DisplayPersonalInfoById(Guid id)
        {
            var result = await _employeeService.DisplayPersonalInfoById(id);
            return Ok(result);
        }



    }
}
