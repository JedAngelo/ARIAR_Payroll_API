using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payroll_Library.Models.Dto.UserAuthDto;
using Payroll_Library.Models.Dto;
using Payroll_Library.UserAuth;

namespace ARIAR_Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _authService;

        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult<ApiResponse<string>>> RegisterAdmin([FromBody] RegisterModelDto param)
        {
            var result = await _authService.RegisterAdminAsync(param);
            return Ok(result);
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModelDto param)
        {
            var result = await _authService.RegisterUserAsync(param);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<UserLoginDto>>> Login([FromBody] LoginModelDto param)
        {
            var result = await _authService.LoginAsync(param);
            return Ok(result);
        }
    }
}
