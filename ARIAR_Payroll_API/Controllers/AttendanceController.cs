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


        [HttpPost("LogAttendance")]
        public async Task<ActionResult<ApiResponse<string>>> LogAttendance(AttendanceDto dto)
        {
            var result = await _attendanceService.LogAttendance(dto);
            return Ok(result);

        }

        [HttpPost("HasMorningIn")]
        public async Task<ActionResult<ApiResponse<bool>>> HasMorningIn(AttendanceDto dto)
        {
            var result = await _attendanceService.HasMorningIn(dto);
            return Ok(result);
        }

        [HttpGet("GetAllAttendanceShort/{date}")]
        public async Task<ActionResult<ApiResponse<List<AttendanceDisplayDto>>>> GetAllAttendanceShort(DateOnly date)
        {
            var result = await _attendanceService.GetAllAttendanceShort(date);
            return Ok(result);
        }

        [HttpGet("GetLogCount/{date}")]
        public async Task<ActionResult<ApiResponse<LogCountDto>>> CountLog(DateOnly date)
        {
            var result = await _attendanceService.CountLog(date);
            return Ok(result);
        }
    }

}
