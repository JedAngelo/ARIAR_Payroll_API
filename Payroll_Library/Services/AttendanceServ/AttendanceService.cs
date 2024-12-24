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
                var _apiSuccessMessage = "";
                var _apiErrorMessage = "";
                var _apiSuccess = false;
                var _settings = await _context.SystemSettings.FirstOrDefaultAsync(x => x.SettingsId == 1);


                var _checkDateLeaves = await _context.Attendances.Where(x => x.AttendanceDate == dto.AttendanceDate && x.Status == "LEAVE").ToListAsync();
                foreach (var onLeave in _checkDateLeaves)
                {
                    onLeave.MorningIn = new TimeOnly(8, 0, 0);
                    onLeave.MorningOut = new TimeOnly(12, 0, 0);
                    onLeave.AfternoonIn = new TimeOnly(13, 0, 0);
                    onLeave.AfternoonOut = new TimeOnly(17, 0, 0);

                    onLeave.DayType = dto.DayType;
                    onLeave.PayMultiplier = dto.PayMultiplier;
                }
                var employeeIdsOnLeave = _checkDateLeaves.Select(l => l.PersonalId).ToList();

                var _currentDateLog = await _context.Attendances.AnyAsync(a => a.AttendanceDate == dto.AttendanceDate && (a.MorningIn == null && a.AfternoonIn == null && a.MorningOut == null && a.AfternoonOut == null) && !employeeIdsOnLeave.Contains(a.PersonalId));
                if (!_currentDateLog)
                {
                    var employees = await _context.PersonalInformations.Where(e => !employeeIdsOnLeave.Contains(e.PersonalId)).ToListAsync();
                    foreach (var employee in employees)
                    {
                        var _initAttendance = new Attendance
                        {
                            PersonalId = employee.PersonalId,
                            AttendanceDate = dto.AttendanceDate,
                            DayType = dto.DayType,
                            PayMultiplier = dto.PayMultiplier,
                            Status = "ABSENT"
                        };
                        await _context.Attendances.AddAsync(_initAttendance);
                    }
                    await _context.SaveChangesAsync();
                }

                if (_settings!.AttendanceType == "FULL")
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(x => x.PersonalId == dto.PersonalId && x.AttendanceDate == dto.AttendanceDate);

                    if (_existingLog != null)
                    {

                        if (dto.MorningIn != null && _existingLog.MorningIn == null)
                        {
                            _existingLog.MorningIn = dto.MorningIn;
                            _existingLog.Status = "TIME IN(AM)";
                        }

                        if (dto.MorningOut != null && _existingLog.MorningOut == null)
                        {
                            _existingLog.MorningOut = dto.MorningOut;
                            _existingLog.Status = "TIME OUT(AM)";
                        }

                        if (dto.AfternoonIn != null && _existingLog.AfternoonIn == null)
                        {
                            _existingLog.AfternoonIn = dto.AfternoonIn;
                            _existingLog.Status = "TIME IN(PM)";
                        }

                        if (dto.AfternoonOut != null && _existingLog.AfternoonOut == null)
                        {
                            _existingLog.AfternoonOut = dto.AfternoonOut;
                            _existingLog.Status = "TIME OUT(PM)";
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                if (_settings!.AttendanceType == "IN/OUT")
                {
                    var _existingLog = await _context.Attendances.FirstOrDefaultAsync(x => x.PersonalId == dto.PersonalId && x.AttendanceDate == dto.AttendanceDate);

                    if (_existingLog != null)
                    {

                        if (dto.MorningIn != null && _existingLog.MorningIn == null)
                        {
                            _existingLog.MorningIn = dto.MorningIn;
                            _existingLog.Status = "IN";
                        }

                        if (dto.MorningOut != null && _existingLog.MorningOut == null)
                        {
                            _existingLog.MorningOut = dto.MorningOut;
                            _existingLog.Status = "OUT";
                        }

                        if (dto.AfternoonIn != null && _existingLog.AfternoonIn == null)
                        {
                            _existingLog.AfternoonIn = dto.AfternoonIn;
                            _existingLog.Status = "IN";
                        }

                        if (dto.AfternoonOut != null && _existingLog.AfternoonOut == null)
                        {
                            _existingLog.AfternoonOut = dto.AfternoonOut;
                            _existingLog.Status = "OUT";
                        }

                        await _context.SaveChangesAsync();
                    }
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
                    AbsentCount = _attendanceData.Count(x => x.Status == "ABSENT"),
                    LeaveCount = _attendanceData.Count(x => x.Status == "LEAVE")
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
