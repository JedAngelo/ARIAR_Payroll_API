using Payroll_Library.Models;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Services.Employee
{
    public class BiomentricService : IBiomentricService
    {
        private readonly AriarPayrollDbContext _context;

        public BiomentricService(AriarPayrollDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<string>> AddBiometricData(EmployeeBiometricDto dto)
        {
            try
            {
                if (dto.PersonalId != null)
                {
                    var _biometricData = new EmployeeBiometric()
                    {
                        RecordId = 0,
                        BiometricData = dto.BiometricData,
                        RecordDate = dto.RecordDate,
                        PersonalId = (Guid)dto.PersonalId,
                    };

                    await _context.AddAsync(_biometricData);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<string>
                    {
                        Data = "Successfully added biometric data",
                        ErrorMessage = "",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Data = "",
                        ErrorMessage = "No personal id supplied!",
                        IsSuccess = false
                    };
                }

            }
            catch (Exception ex)
            {

                return new ApiResponse<string>
                {
                    Data = "",
                    ErrorMessage = $"Error: {ex.Message}  | Inner Exception: {ex.InnerException?.Message}",
                    IsSuccess = false,
                };
            }
        }


        public async Task<ApiResponse<List<EmployeeBiometricDisplayDto>>> DisplayBiometricData()
        {
            try
            {
                var _employeeBiometric = _context.EmployeeBiometrics.Select(x => new EmployeeBiometricDisplayDto()
                {
                    PersonalId = x.PersonalId,
                    BiometricData = x.BiometricData
                }).ToList();

                return new ApiResponse<List<EmployeeBiometricDisplayDto>>
                {
                    Data = _employeeBiometric,
                    ErrorMessage = "",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<EmployeeBiometricDisplayDto>>
                {
                    Data = [],
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false
                };
                
            }
        }
    }
}
