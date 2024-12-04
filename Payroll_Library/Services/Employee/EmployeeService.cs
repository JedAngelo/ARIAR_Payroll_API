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
            _context = context;
        }


        public async Task<ApiResponse<string>> AddOrUpdateEmployeeInfo(PersonalInformationDto dto)
        {
            try
            {
                if (dto.PersonalId == null)
                {
                    var positionExists = await _context.Positions.AnyAsync(p => p.PositionId == dto.EmploymentDetailDtos.PositionId);
                    if (!positionExists)
                    {
                        return new ApiResponse<string>
                        {
                            Data = "",
                            ErrorMessage = "Error: The PositionId does not exist in the Position table.",
                            IsSuccess = false
                        };
                    }


                    var _contact = dto.ContactInformationDtos;
                    var _employmentDetail = dto.EmploymentDetailDtos;
                    var _personalInfo = new PersonalInformation()
                    {
                        PersonalId = Guid.NewGuid(),
                        FirstName = dto.FirstName,
                        MiddleName = dto.MiddleName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        Gender = dto.Gender,
                        MaritalStatus = dto.MaritalStatus,
                        DateOfBirth = dto.DateOfBirth,
                        //EmployeeImage = dto.EmployeeImage,
                        ContactInformation = new ContactInformation
                        {
                            ContactId = 0,
                            Address = _contact.Address,
                            Email = _contact.Email,
                            PhoneNumber = _contact.PhoneNumber

                        },
                        EmploymentDetail = new EmploymentDetail
                        {
                            EmploymentId = 0,
                            HireDate = _employmentDetail.HireDate,
                            IncomeTaxRate = _employmentDetail.IncomeTaxRate,
                            PagibigEmployeeRate = _employmentDetail.PagibigEmployeeRate,
                            SssEmployeeRate = _employmentDetail.SssEmployeeRate,
                            PhilhealthEmployeeRate = _employmentDetail.PhilhealthEmployeeRate,
                            PayRate = _employmentDetail.PayRate,
                            PositionId = _employmentDetail.PositionId
                        },
                        CreatedBy = dto.CreatedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = dto.IsActive,
                        IsDeleted = false,
                    };

                    if (dto.EmployeeImage != null)
                    {
                        _personalInfo.EmployeeImage = dto.EmployeeImage;
                    }

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
                        .Include(pi => pi.ContactInformation)
                        .Include(pi => pi.EmploymentDetail)
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

                    var positionExists = await _context.Positions.AnyAsync(p => p.PositionId == dto.EmploymentDetailDtos.PositionId);
                    if (!positionExists)
                    {
                        return new ApiResponse<string>
                        {
                            Data = "",
                            ErrorMessage = "Error: The PositionId does not exist in the Position table.",
                            IsSuccess = false
                        };
                    }

                    // Update personal info fields
                    _existingPersonalInfo.FirstName = dto.FirstName;
                    _existingPersonalInfo.MiddleName = dto.MiddleName;
                    _existingPersonalInfo.LastName = dto.LastName;
                    _existingPersonalInfo.Age = dto.Age;
                    _existingPersonalInfo.Gender = dto.Gender;
                    _existingPersonalInfo.MaritalStatus = dto.MaritalStatus;
                    _existingPersonalInfo.DateOfBirth = dto.DateOfBirth;
                    _existingPersonalInfo.ModifiedBy = dto.ModifiedBy;
                    _existingPersonalInfo.ModifiedDate = dto.ModifiedDate;
                    _existingPersonalInfo.IsActive = dto.IsActive;
                    _existingPersonalInfo.IsDeleted = false;

                    // Update existing contact information
                    var _newContact = dto.ContactInformationDtos; // This is expected to be a single DTO
                    var _existingContact = _existingPersonalInfo.ContactInformation;
                    if (_newContact != null)
                    {
                        _existingContact!.Address = _newContact.Address;
                        _existingContact.PhoneNumber = _newContact.PhoneNumber;
                    }


                    // Update existing employment details
                    var _newEmployment = dto.EmploymentDetailDtos;
                    var _existingEmployment = _existingPersonalInfo.EmploymentDetail;
                    if (_newEmployment != null)
                    {
                        _existingEmployment!.HireDate = _newEmployment.HireDate;
                        _existingEmployment.IncomeTaxRate = _newEmployment.IncomeTaxRate;
                        _existingEmployment.PagibigEmployeeRate = _newEmployment.PagibigEmployeeRate;
                        _existingEmployment.PayRate = _newEmployment.PayRate;
                    }


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

        public async Task<ApiResponse<List<PersonalInformationDisplayDto>>> DisplayPersonalInfo()
        {
            try
            {
                var _personalInfo = await _context.PersonalInformations
                                          .Include(c => c.ContactInformation)
                                          .Include(e => e.EmploymentDetail)
                                          .Include(p => p.EmploymentDetail!.Position)
                                          .Select(x => new PersonalInformationDisplayDto
                                          {
                                              PersonalId = x.PersonalId,
                                              FirstName = x.FirstName,
                                              MiddleName = x.MiddleName,
                                              LastName = x.LastName,
                                              Age = x.Age,
                                              DateOfBirth = x.DateOfBirth,
                                              Gender = x.Gender,
                                              MaritalStatus = x.MaritalStatus,
                                              EmployeeImage = x.EmployeeImage,
                                              PhoneNumber = x.ContactInformation.PhoneNumber,
                                              Address = x.ContactInformation.Address,
                                              Email = x.ContactInformation.Email,
                                              HireDate = x.EmploymentDetail.HireDate,
                                              PayRate = x.EmploymentDetail.PayRate,
                                              IncomeTaxRate = x.EmploymentDetail.IncomeTaxRate,
                                              SssEmployeeRate = x.EmploymentDetail.SssEmployeeRate,
                                              PagibigEmployeeRate = x.EmploymentDetail.PagibigEmployeeRate,
                                              PhilhealthEmployeeRate = x.EmploymentDetail.PhilhealthEmployeeRate,
                                              PositionId = x.EmploymentDetail.PositionId,
                                              PositionName = x.EmploymentDetail.Position.PositionName                                            
                                          }).ToListAsync();

                return new ApiResponse<List<PersonalInformationDisplayDto>>
                {
                    Data = _personalInfo,
                    ErrorMessage = "",
                    IsSuccess = true
                };


            }
            catch (Exception ex)
            {

                return new ApiResponse<List<PersonalInformationDisplayDto>>
                {
                    Data = [],
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess= false
                };
            }
        }

        public async Task<ApiResponse<PersonalInformationDisplayDto>> DisplayPersonalInfoById(Guid id)
        {
            try
            {
                var _personalInfo = await _context.PersonalInformations
                                          .Where(p => p.PersonalId == id)
                                          .Include(c => c.ContactInformation)
                                          .Include(e => e.EmploymentDetail)
                                          .Include(p => p.EmploymentDetail!.Position)
                                          .Select(x => new PersonalInformationDisplayDto
                                          {
                                              PersonalId = x.PersonalId,
                                              FirstName = x.FirstName,
                                              MiddleName = x.MiddleName,
                                              LastName = x.LastName,
                                              Age = x.Age,
                                              DateOfBirth = x.DateOfBirth,
                                              Gender = x.Gender,
                                              EmployeeImage = x.EmployeeImage,
                                              PhoneNumber = x.ContactInformation!.PhoneNumber,
                                              Address = x.ContactInformation.Address,
                                              Email = x.ContactInformation.Email,
                                              HireDate = x.EmploymentDetail!.HireDate,
                                              PayRate = x.EmploymentDetail.PayRate,
                                              IncomeTaxRate = x.EmploymentDetail.IncomeTaxRate,
                                              SssEmployeeRate = x.EmploymentDetail.SssEmployeeRate,
                                              PagibigEmployeeRate = x.EmploymentDetail.PagibigEmployeeRate,
                                              PhilhealthEmployeeRate = x.EmploymentDetail.PhilhealthEmployeeRate,
                                              PositionId = x.EmploymentDetail.PositionId,
                                              PositionName = x.EmploymentDetail.Position.PositionName
                                          }).FirstOrDefaultAsync();

                if (_personalInfo != null)
                {
                    return new ApiResponse<PersonalInformationDisplayDto>
                    {
                        Data = _personalInfo,
                        ErrorMessage = "",
                        IsSuccess = true
                    };
                }

                return new ApiResponse<PersonalInformationDisplayDto>
                {
                    Data = new PersonalInformationDisplayDto(),
                    ErrorMessage = "Personal ID does not exist",
                    IsSuccess = false
                };


            }
            catch (Exception ex)
            {
                return new ApiResponse<PersonalInformationDisplayDto>
                {
                    Data = new PersonalInformationDisplayDto(),
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

    }
}
