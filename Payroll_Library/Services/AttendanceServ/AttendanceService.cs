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



        public async Task<ApiResponse<string>> LogAttendance(AttendanceDto dto)
        {
            try
            {
                var halfDay = "HALF DAY";
                var fullDay = "FULL DAY";
                var leave = "LEAVE";

                var _apiMessage = "";

                if (dto.MorningIn != null)
                {
                    var _existingLog = await _context.Attendances.AnyAsync(a => a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate);
                    if (_existingLog)
                    {
                        _apiMessage = "Invalid time in, you already have a time in data";
                    }
                    else
                    {
                        var _morningIn = new Attendance
                        {
                            AttendanceId = 0,
                            PersonalId = dto.PersonalId,
                            MorningIn = dto.MorningIn,
                            AttendanceDate = dto.AttendanceDate
                        };

                        await _context.Attendances.AddAsync(_morningIn);
                        await _context.SaveChangesAsync();
                        _apiMessage = "Log morning in";
                    }
                }
                if (dto.AfternoonIn != null)
                {
                    var _existingLog = await _context.Attendances.AnyAsync(a => a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate);
                    if (_existingLog)
                    {
                        _apiMessage = "Invalid time in, you already have time in data";
                    }
                    else
                    {
                        var _afternoonIn = new Attendance
                        {
                            AttendanceId = 0,
                            PersonalId = dto.PersonalId,
                            AfternoonIn = dto.AfternoonIn,
                            AttendanceDate = dto.AttendanceDate,
                            Status = halfDay
                        };

                        await _context.Attendances.AddAsync(_afternoonIn);
                        await _context.SaveChangesAsync();
                        _apiMessage = "Log afternoon in";
                    }
                }
                if (dto.MorningOut != null)
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(a => a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate && a.MorningOut == null);
                    if (_existingLog != null)
                    {
                        _existingLog.MorningOut = dto.MorningOut;
                        _existingLog.Status = halfDay;
                        _context.Attendances.Update(_existingLog);
                        await _context.SaveChangesAsync();
                        _apiMessage = "Log morning out, logged as half day";
                    }
                    else
                    {
                        _apiMessage = "Invalid log out, you already logged out";
                    }
                }
                if (dto.AfternoonOut != null)
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(a => a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate && a.MorningOut == null && a.AfternoonOut == null);
                    if (_existingLog != null)
                    {
                        _existingLog.AfternoonOut = dto.AfternoonOut;
                        _existingLog.Status = fullDay;
                        _context.Attendances.Update(_existingLog);
                        await _context.SaveChangesAsync();
                        if (_existingLog.MorningIn != null)
                        {
                            _apiMessage = "Log afternoon out, logged as full day";
                        }
                        else
                        {
                            _apiMessage = "Log afternoon out, logged as half day";
                        }
                    }
                    else
                    {
                        _apiMessage = "Invalid log out, you already logged out";
                    }
                }
                if (dto.Status == leave)
                {
                    var _leaveAttendance = new Attendance
                    {
                        AttendanceId = 0,
                        PersonalId = dto.PersonalId,
                        AttendanceDate = dto.AttendanceDate,
                        Status = leave
                    };

                    await _context.Attendances.AddAsync(_leaveAttendance);
                    await _context.SaveChangesAsync();

                    _apiMessage = "Logged as on leave";
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

        public async Task<ApiResponse<bool>> HasMorningIn(AttendanceDto dto)
        {
            try
            {
                bool _hasMorningIn = await _context.Attendances.AnyAsync(a => a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate && a.MorningIn != null);

                return new ApiResponse<bool>
                {
                    Data = _hasMorningIn,
                    ErrorMessage = "",
                    IsSuccess = true
                };


            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false

                };
            }
        }

        public async Task<ApiResponse<List<AttendanceDisplayDto>>> GetAllAttendanceShort(DateOnly date)
        {
            try
            {

                var _attendanceData = await _context.Attendances.Include(a => a.Personnel).Where(a => a.AttendanceDate == date).ToListAsync();

                var _attendanceDisplay = new List<AttendanceDisplayDto>();

                foreach (var attendance in _attendanceData)
                {
                    var personnel = attendance.Personnel;
                   
                    if (attendance.MorningIn != null)
                    {
                        _attendanceDisplay.Add(new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personnel.LastName}, {attendance.Personnel.FirstName} {(string.IsNullOrEmpty(attendance.Personnel.MiddleName) ? "" : attendance.Personnel.MiddleName[0].ToString())}.",
                            Log = attendance.MorningIn,
                            Type = "TIME IN - AM"
                        });
                    }

                    if (attendance.MorningOut != null)
                    {
                        _attendanceDisplay.Add(new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personnel.LastName}, {attendance.Personnel.FirstName} {(string.IsNullOrEmpty(attendance.Personnel.MiddleName) ? "" : attendance.Personnel.MiddleName[0].ToString())}.",
                            Log = attendance.MorningOut,
                            Type = "TIME OUT - AM (HALF DAY)"
                        });
                    }

                    if (attendance.AfternoonIn != null)
                    {
                        _attendanceDisplay.Add(new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personnel.LastName}, {attendance.Personnel.FirstName} {(string.IsNullOrEmpty(attendance.Personnel.MiddleName) ? "" : attendance.Personnel.MiddleName[0].ToString())}.",
                            Log = attendance.AfternoonIn,
                            Type = "TIME IN - PM"
                        });
                    }

                    if (attendance.AfternoonOut != null)
                    {
                        var _type = "TIME OUT - PM (FULL DAY)";
                        if (attendance.AfternoonIn != null)
                        {
                            _type = "TIME OUT - PM (HALF DAY)";
                        }
                        _attendanceDisplay.Add(new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personnel.LastName}, {attendance.Personnel.FirstName} {(string.IsNullOrEmpty(attendance.Personnel.MiddleName) ? "" : attendance.Personnel.MiddleName[0].ToString())}.",
                            Log = attendance.AfternoonOut,
                            Type = _type
                        });
                    }
                    
                }

                return new ApiResponse<List<AttendanceDisplayDto>>
                {
                    Data = _attendanceDisplay,
                    ErrorMessage = "",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AttendanceDisplayDto>>
                {
                    Data = [],
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false
                };
            }
        }



    }
}
