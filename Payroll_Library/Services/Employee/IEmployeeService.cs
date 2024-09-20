using Payroll_Library.Models.Dto;

namespace Payroll_Library.Services.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResponse<string>> AddEmployeeInfo(PersonalInformationDto dto);
        Task<ApiResponse<string>> AddPosition(PostionDto dto);
    }
}