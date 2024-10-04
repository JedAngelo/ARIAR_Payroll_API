using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.UserAuthDto;

namespace Payroll_Library.UserAuth
{
    public interface IUserAuthenticationService
    {
        Task<ApiResponse<UserLoginDto>> LoginAsync(LoginModelDto param);
        Task<ApiResponse<string>> RegisterAdminAsync(RegisterModelDto param);
        Task<ApiResponse<string>> RegisterUserAsync(RegisterModelDto param);
    }
}