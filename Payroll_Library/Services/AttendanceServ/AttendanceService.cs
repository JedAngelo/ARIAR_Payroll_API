using Microsoft.EntityFrameworkCore;
using Payroll_Library.Models;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.AttendanceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Services.AttendanceServ
{
    public class AttendanceService : IAttendanceService
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
                var _apiMessage = "";
                if (dto.AttendanceId == null)
                {
                    var _newTimeIn = new Attendance()
                    {
                        AttendanceId = 0,
                        PersonalId = dto.PersonalId,
                        MorningIn = dto.MorningIn,
                        AfternoonIn = dto.AfternoonIn,
                    };

                    await _context.AddAsync(_newTimeIn);
                    await _context.SaveChangesAsync();

                    _apiMessage = "Time in logged";
                }
                else
                {
                    var _existingAttendance = await _context.Attendances
                        .FirstOrDefaultAsync(x => x.AttendanceId == dto.AttendanceId);


                    _existingAttendance.MorningOut = dto.MorningOut;
                    _existingAttendance.AfternoonOut = dto.AfternoonOut;



                    _context.Update(_existingAttendance);
                    await _context.SaveChangesAsync();

                    _apiMessage = "Time out logged";
                }

                return new ApiResponse<string>
                {
                    Data = _apiMessage,
                    ErrorMessage = "",
                    IsSuccess = true
                };

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
