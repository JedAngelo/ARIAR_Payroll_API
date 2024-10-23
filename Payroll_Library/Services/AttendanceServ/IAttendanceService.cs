using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.AttendanceDto;

namespace Payroll_Library.Services.AttendanceServ
{
    public interface IAttendanceService
    {
        Task<ApiResponse<string>> LogAttendance(AttendanceDto dto);
        Task<ApiResponse<bool>> HasMorningIn(AttendanceDto dto);
        Task<ApiResponse<List<AttendanceDisplayDto>>> GetAllAttendanceShort(DateOnly date);
    }
}