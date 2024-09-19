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

        public async Task<ApiResponse<string>> InsertOrUpdateEmployeeInfo(PersonalInformationDto dto)
        {
            try
            {

                var _personalInfo = new PersonalInformation()
                {
                    PersonalId = Guid.NewGuid(),
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Age = dto.Age,
                    Gender = dto.Gender,
                    ContactInformations = dto.ContactInformationDtos.Select(c => new ContactInformation
                    {
                        Address = c.Address,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                    }).ToList(),
                    EmployeeBiometrics = dto.EmployeeBiometricDtos.Select(b => new EmployeeBiometric
                    {
                        BiometricData = b.BiometricData,
                        RecordDate = DateTime.Now,
                        RecordId = 0,
                    }).ToList(),
                    EmploymentDetails = dto.EmploymentDetailDtos.Select(d => new EmploymentDetail
                    {
                        EmploymentId = 0,
                        HireDate = d.HireDate,
                        IncomeTaxRate = d.IncomeTaxRate,
                        PagibigEmployeeRate = d.PagibigEmployeeRate,
                        PayRate = d.PayRate,
                        
                    }).ToList(),
                    CreatedBy = dto.CreatedBy,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    IsActive = dto.IsActive
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
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false,
                };

            }
        }
    }
}
