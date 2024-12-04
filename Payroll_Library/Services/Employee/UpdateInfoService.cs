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
    public class UpdateInfoService
    {
        private readonly AriarPayrollDbContext _context;

        public UpdateInfoService(AriarPayrollDbContext context)
        {
            _context = context;
        }


        public async Task<ApiResponse<string>> UpdateContactInfo(PersonalInformationDto dto)
        {
            try
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

                //_existingPersonalInfo.ContactInformations = dto.ContactInformationDtos.Select(c => new ContactInformation
                //{
                //    ContactId = c.ContactId,
                //    Address = c.Address,
                //    Email = c.Email,
                //    PhoneNumber = c.PhoneNumber,
                //}).ToList();
                var _contact = dto.ContactInformationDtos;
                _existingPersonalInfo.ContactInformation = new ContactInformation
                {
                    ContactId = _contact.ContactId,
                    Address = _contact.Address,
                    Email = _contact.Email,
                    PhoneNumber = _contact.PhoneNumber
                };

                _existingPersonalInfo.ModifiedBy = dto.ModifiedBy;
                _existingPersonalInfo.ModifiedDate = DateTime.Now;
                


                _context.Update(_existingPersonalInfo);
                await _context.SaveChangesAsync();


                return new ApiResponse<string>
                {
                    Data = "Updated contact info",
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


        public async Task<ApiResponse<string>> UpdateEmploymentDetail(PersonalInformationDto dto)
        {
            try
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

                var _employmentDetails = dto.EmploymentDetailDtos;
                _existingPersonalInfo.EmploymentDetail = new EmploymentDetail
                {
                    EmploymentId = _employmentDetails.EmploymentId,
                    HireDate = _employmentDetails.HireDate,
                    IncomeTaxRate = _employmentDetails.IncomeTaxRate,
                    PagibigEmployeeRate = _employmentDetails.PagibigEmployeeRate,
                    PayRate = _employmentDetails.PayRate,
                    PositionId = _employmentDetails.PositionId
                };

                _context.Update(_existingPersonalInfo);
                await _context.SaveChangesAsync();


                return new ApiResponse<string>
                {
                    Data = "Updated employment details",
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
