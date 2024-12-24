using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.EmployeeDto;
using Payroll_Library.Services.EmployeeServ;

namespace ARIAR_Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiometricController : ControllerBase
    {
        private readonly IBiomentricService _biomentricService;

        public BiometricController(IBiomentricService biomentricService)
        {
            _biomentricService = biomentricService;
        }
        [HttpPost("AddBiometricData")]
        public async Task<ActionResult<ApiResponse<string>>> AddBiometricData(EmployeeBiometricDto dto)
        {
            var result = await _biomentricService.AddBiometricData(dto);
            return Ok(result);
        }

        [HttpGet("DisplayEmployeeBiometric")]
        public async Task<ActionResult<ApiResponse<List<EmployeeBiometricDisplayDto>>>> DisplayBiometricData()
        {
            var result = await _biomentricService.DisplayBiometricData();
            return Ok(result);
        }
    }
}
