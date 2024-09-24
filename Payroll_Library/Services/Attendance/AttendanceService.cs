using Payroll_Library.Models;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.AttendanceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Services.Attendance
{
    public class AttendanceService
    {
        private readonly AriarPayrollDbContext _context;

        public AttendanceService(AriarPayrollDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<string>> AddAttendance(AttendanceDto dto)
        {
            try
            {
                if (dto.MorningIn == null)
                {

                }

            }
            catch (Exception ex)
            {


                return new ApiResponse<string>
                {
                    Data = "",
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false
                };
            }

        }
    }
}
