using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.AttendanceDto;

namespace Payroll_Library.Services.AttendanceServ
{
    public interface IAttendanceService
    {
        Task<ApiResponse<string>> AddAttendance(AttendanceDto dto);
    }
}