using Payroll_Library.Models;
using Payroll_Library.Models.Dto;
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

        public async Task<ApiResponse<string>> AddEmployeeInfo(PersonalInformationDto dto)
        {
            try
            {
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
    }
}
