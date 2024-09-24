using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll_Library.Models.Dto.EmployeeDto;
using Payroll_Library.Models.Dto;
using Payroll_Library.Services.AttendanceServ;
using Payroll_Library.Models.Dto.AttendanceDto;

namespace ARIAR_Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }


        [HttpPost("AddAttendance")]
        public async Task<ActionResult<ApiResponse<string>>> AddAttendance(AttendanceDto dto)
        {
            var result = await _attendanceService.AddAttendance(dto);
            return Ok(result);

        }
    }

}
