using Microsoft.EntityFrameworkCore;
using Payroll_Library.Models;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.AttendanceDto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

                var _apiSuccessMessage = "";
                var _apiErrorMessage = "";

                var _apiSuccess = false;

                var _currentDateLog = await _context.Attendances.AnyAsync(a => a.AttendanceDate == dto.AttendanceDate);
                if (!_currentDateLog)
                {
                    var employees = await _context.PersonalInformations.ToListAsync();
                    foreach (var employee in employees)
                    {
                        var _initAttendance = new Attendance
                        {
                            PersonalId = employee.PersonalId,
                            AttendanceDate = dto.AttendanceDate
                        };
                        await _context.Attendances.AddAsync(_initAttendance);
                    }
                    await _context.SaveChangesAsync();
                }

                if (dto.MorningIn != null)
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(a => a.MorningIn == null && a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate);
                    
                    if (_existingLog == null)
                    {
                        _apiErrorMessage = "Invalid time in, you already have a time in data";
                    }
                    else
                    {
                        _existingLog.MorningIn = dto.MorningIn;

                        await _context.SaveChangesAsync();
                        _apiSuccessMessage = "Log morning in";
                        _apiSuccess = true;
                    }
                }
                if (dto.AfternoonIn != null)
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(a => a.MorningIn == null || a.AfternoonIn == null && a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate);
                    
                    if (_existingLog == null)
                    {
                        _apiErrorMessage = "Invalid time in, you already have time in data";
                    }
                    else
                    {
                        _existingLog.AfternoonIn = dto.AfternoonIn;

                        await _context.SaveChangesAsync();
                        _apiSuccessMessage = "Log afternoon in";
                        _apiSuccess = true;
                    }
                }
                if (dto.MorningOut != null)
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(a => a.PersonalId == dto.PersonalId && a.AttendanceDate == dto.AttendanceDate && a.MorningOut == null);
                    if (_existingLog != null)
                    {
                        _existingLog.MorningOut = dto.MorningOut;
                        _existingLog.Status = halfDay;
                        await _context.SaveChangesAsync();
                        _apiSuccessMessage = "Log morning out, logged as half day";
                        _apiSuccess = true;
                    }
                    else
                    {
                        _apiErrorMessage = "Invalid log out, you already logged out";
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
                            _apiSuccessMessage = "Log afternoon out, logged as full day";
                        }
                        else
                        {
                            _apiSuccessMessage = "Log afternoon out, logged as half day";
                        }
                        _apiSuccess = true;
                    }
                    else
                    {
                        _apiErrorMessage = "Invalid log out, you already logged out";
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

                    _apiSuccessMessage = "Logged as on leave";
                    _apiSuccess = true;
                }

                return new ApiResponse<string>
                {
                    Data = _apiSuccessMessage,
                    ErrorMessage = _apiErrorMessage,
                    IsSuccess = _apiSuccess
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

                var _attendanceData = await _context.Attendances.Include(a => a.Personal).Where(a => a.AttendanceDate == date).ToListAsync();

                var _attendanceDisplay = new List<AttendanceDisplayDto>();

                foreach (var attendance in _attendanceData)
                {
                    var Personal = attendance.Personal;
                   
                    if (attendance.MorningIn != null)
                    {
                        _attendanceDisplay.Add( new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personal.LastName}, {attendance.Personal.FirstName} {(string.IsNullOrEmpty(attendance.Personal.MiddleName) ? "" : attendance.Personal.MiddleName[0].ToString())}.",
                            Log = attendance.MorningIn,
                            Type = "TIME IN - AM",
                            EmployeeImage = attendance.Personal.EmployeeImage!
                        });
                    }

                    if (attendance.MorningOut != null)
                    {
                        _attendanceDisplay.Add(new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personal.LastName}, {attendance.Personal.FirstName} {(string.IsNullOrEmpty(attendance.Personal.MiddleName) ? "" : attendance.Personal.MiddleName[0].ToString())}.",
                            Log = attendance.MorningOut,
                            Type = "TIME OUT - AM (HALF DAY)",
                            EmployeeImage = attendance.Personal.EmployeeImage!
                        });
                    }

                    if (attendance.AfternoonIn != null)
                    {
                        _attendanceDisplay.Add(new AttendanceDisplayDto
                        {
                            Name = $"{attendance.Personal.LastName}, {attendance.Personal.FirstName} {(string.IsNullOrEmpty(attendance.Personal.MiddleName) ? "" : attendance.Personal.MiddleName[0].ToString())}.",
                            Log = attendance.AfternoonIn,
                            Type = "TIME IN - PM",
                            EmployeeImage = attendance.Personal.EmployeeImage!
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
                            Name = $"{attendance.Personal.LastName}, {attendance.Personal.FirstName} {(string.IsNullOrEmpty(attendance.Personal.MiddleName) ? "" : attendance.Personal.MiddleName[0].ToString())}.",
                            Log = attendance.AfternoonOut,
                            Type = _type,
                            EmployeeImage = attendance.Personal.EmployeeImage!
                        });
                    }
                    
                }
                _attendanceDisplay = _attendanceDisplay.OrderBy(a => a.Log).ToList();

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

        public async Task<ApiResponse<LogCountDto>> CountLog(DateOnly date)
        {
            try
            {

                var _attendanceData = await _context.Attendances.Where(a => a.AttendanceDate == date).ToListAsync();

                if (_attendanceData.Count == 0)
                {
                    return new ApiResponse<LogCountDto>
                    {
                        Data = null!,
                        ErrorMessage = "Attendance data not found",
                        IsSuccess = false
                    };
                }

                var _currentDate = DateOnly.FromDateTime(DateTime.Now);
                var _counts = new LogCountDto
                {
                    PresentCount = _attendanceData.Count(x => x.Status == "FULL DAY" || x.Status == "HALF DAY"),
                    AbsentCount = _attendanceData.Count(x => x.Status == null && _currentDate > x.AttendanceDate),
                    LeaveCount = _attendanceData.Count(x => x.Status == "ON LEAVE")
                };

                return new ApiResponse<LogCountDto>
                {
                    Data = _counts,
                    ErrorMessage = "",
                    IsSuccess = true
                };


            }
            catch (Exception ex)
            {

                return new ApiResponse<LogCountDto>
                {
                    Data = new LogCountDto(),
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
        }

    }
}
