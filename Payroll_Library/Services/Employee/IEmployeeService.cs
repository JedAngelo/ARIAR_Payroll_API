using Payroll_Library.Models.Dto;

namespace Payroll_Library.Services.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResponse<string>> InsertOrUpdateEmployeeInfo(PersonalInformationDto dto);
    }
}