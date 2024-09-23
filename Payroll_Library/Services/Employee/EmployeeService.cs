using Microsoft.EntityFrameworkCore;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly AriarPayrollDbContext _context;

        public EmployeeService(AriarPayrollDbContext context)
        {
            this._context = context;
        }

        public async Task<ApiResponse<string>> AddOrUpdateEmployeeInfo(PersonalInformationDto dto)
        {
            try
            {
                if (dto.PersonalId == null)
                {
                    var positionExists = await _context.Positions.AnyAsync(p => p.PositionId == dto.EmploymentDetailDtos[0].PositionId);
                    if (!positionExists)
                    {
                        return new ApiResponse<string>
                        {
                            Data = "",
                            ErrorMessage = "Error: The PositionId does not exist in the Position table.",
                            IsSuccess = false
                        };
                    }

                    var _personalInfo = new PersonalInformation()
                    {
                        PersonalId = Guid.NewGuid(),
                        FirstName = dto.FirstName,
                        MiddleName = dto.MiddleName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        Gender = dto.Gender,
                        DateOfBirth = dto.DateOfBirth,
                        ContactInformations = dto.ContactInformationDtos.Select(c => new ContactInformation
                        {
                            ContactId = 0, 
                            Address = c.Address,
                            Email = c.Email,
                            PhoneNumber = c.PhoneNumber,
                        }).ToList(),
                        EmploymentDetails = dto.EmploymentDetailDtos.Select(d => new EmploymentDetail
                        {
                            EmploymentId = 0,
                            HireDate = d.HireDate,
                            IncomeTaxRate = d.IncomeTaxRate,
                            PagibigEmployeeRate = d.PagibigEmployeeRate,
                            PayRate = d.PayRate,
                            PositionId = d.PositionId
                        }).ToList(),
                        CreatedBy = dto.CreatedBy,
                        CreatedDate = dto.CreatedDate,
                        IsActive = dto.IsActive,
                        IsDeleted = false,
                    };

                    await _context.AddAsync(_personalInfo);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<string>
                    {
                        Data = "Success",
                        ErrorMessage = "",
                        IsSuccess = true,
                    };
                }
                else
                {
                    var _existingPersonalInfo = await _context.PersonalInformations
                        
                        .FirstOrDefaultAsync(pi => pi.PersonalId == dto.PersonalId);


                    if (_existingPersonalInfo == null)
                    {
                        return new ApiResponse<string>
                        {
                            Data = "",
                            ErrorMessage = "Error: Employee not found.",
                            IsSuccess = false
                        };
                    }

                    var positionExists = await _context.Positions.AnyAsync(p => p.PositionId == dto.EmploymentDetailDtos[0].PositionId);
                    if (!positionExists)
                    {
                        return new ApiResponse<string>
                        {
                            Data = "",
                            ErrorMessage = "Error: The PositionId does not exist in the Position table.",
                            IsSuccess = false
                        };
                    }

                    _existingPersonalInfo.FirstName = dto.FirstName;
                    _existingPersonalInfo.MiddleName = dto.MiddleName;
                    _existingPersonalInfo.LastName = dto.LastName;
                    _existingPersonalInfo.Age = dto.Age;
                    _existingPersonalInfo.Gender = dto.Gender;
                    _existingPersonalInfo.DateOfBirth = dto.DateOfBirth;
                    _existingPersonalInfo.ModifiedBy = dto.ModifiedBy; 
                    _existingPersonalInfo.ModifiedDate = dto.ModifiedDate; 
                    _existingPersonalInfo.IsActive = dto.IsActive;
                    _existingPersonalInfo.IsDeleted = false;

                    _existingPersonalInfo.ContactInformations = dto.ContactInformationDtos.Select(c => new ContactInformation
                    {
                        Address = c.Address,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                    }).ToList();

                    _existingPersonalInfo.EmploymentDetails = dto.EmploymentDetailDtos.Select(d => new EmploymentDetail
                    {
                        HireDate = d.HireDate,
                        IncomeTaxRate = d.IncomeTaxRate,
                        PagibigEmployeeRate = d.PagibigEmployeeRate,
                        PayRate = d.PayRate,
                        PositionId = d.PositionId
                    }).ToList();

                    _context.PersonalInformations.Update(_existingPersonalInfo);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<string>
                    {
                        Data = "Updated",
                        ErrorMessage = "",
                        IsSuccess = true,
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

        public async Task<ApiResponse<string>> AddPosition(PostionDto dto)
        {
            try
            {
                var _position = new Position
                {
                    PositionId = 0,
                    PositionName = dto.PositionName,
                    CreatedBy = dto.PositionName,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,


                };
                await _context.AddAsync(_position);
                await _context.SaveChangesAsync();

                return new ApiResponse<string>
                {
                    Data = "Position Added",
                    ErrorMessage = "",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {

                return new ApiResponse<string>
                {
                    Data = "Failed adding position",
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<ApiResponse<string>> DeleteEmployee(DeleteEmployeeDto dto)
        {
            try
            {
                var _apiMessage = "";

                var employeeToDelete = await _context.PersonalInformations
                    .FirstOrDefaultAsync(e => e.PersonalId == dto.PersonalId);

                if (employeeToDelete == null)
                {
                    return new ApiResponse<string>
                    {
                        Data = "Employee not found",
                        ErrorMessage = "",
                        IsSuccess = false
                    };
                }

                employeeToDelete.IsDeleted = true; 
                employeeToDelete.DeletedBy = dto.DeletedBy ?? throw new ArgumentNullException(nameof(dto.DeletedBy));
                employeeToDelete.DeletedDate = DateTime.Now;

                _context.PersonalInformations.Update(employeeToDelete);
                await _context.SaveChangesAsync();

                _apiMessage = "Employee successfully deleted";

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
                    Data = "Failed deleting employee",
                    ErrorMessage = $"Error: {ex.Message} Inner Exception: {ex.InnerException?.Message}",
                    IsSuccess = false,
                };
            }
        }

    }
}
