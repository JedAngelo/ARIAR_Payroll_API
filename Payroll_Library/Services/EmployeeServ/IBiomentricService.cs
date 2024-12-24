using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.EmployeeDto;

namespace Payroll_Library.Services.EmployeeServ
{
    public interface IBiomentricService
    {
        Task<ApiResponse<string>> AddBiometricData(EmployeeBiometricDto dto);
        Task<ApiResponse<List<EmployeeBiometricDisplayDto>>> DisplayBiometricData();
    }
}