using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.EmployeeDto;

namespace Payroll_Library.Services.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResponse<string>> AddOrUpdateEmployeeInfo(PersonalInformationDto dto);
        Task<ApiResponse<string>> AddPosition(PostionDto dto);
        Task<ApiResponse<string>> DeleteEmployee(DeleteEmployeeDto dto);
    }
}