using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.AttendanceDto;
using Payroll_Library.Models.Dto.EmployeeDto;

namespace Payroll_Library.Services.EmployeeServ
{
    public interface IEmployeeService
    {
        Task<ApiResponse<string>> AddOrUpdateEmployeeInfo(PersonalInformationDto dto);
        Task<ApiResponse<string>> AddPosition(PostionDto dto);
        Task<ApiResponse<string>> DeleteEmployee(DeleteEmployeeDto dto);
        Task<ApiResponse<List<PersonalInformationDisplayDto>>> DisplayPersonalInfo();
        Task<ApiResponse<PersonalInformationDisplayDto>> DisplayPersonalInfoById(Guid id);
        Task<ApiResponse<PersonalInformationDto>> DisplayPersonalInfoRaw(Guid id);
        Task<ApiResponse<List<PostionDto>>> DisplayPositions();
    }
}