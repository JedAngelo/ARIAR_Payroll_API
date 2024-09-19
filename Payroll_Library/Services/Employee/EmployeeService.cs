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
                    ContactInformations = [],
                    EmployeeBiometrics = [],
                    EmploymentDetails = [],
                    CreatedBy = dto.CreatedBy,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    IsActive = dto.IsActive
                };

                var _contactInfo = new ContactInformation()
                {
                    ContactId = 0,
                    Address = dto.ContactInformationDtos.FirstOrDefault().Address,
                    Email = dto.ContactInformationDtos.FirstOrDefault().Email,
                    PhoneNumber = dto.ContactInformationDtos.FirstOrDefault().PhoneNumber

                };

                var _employeeBio = new EmployeeBiometric()
                {
                    RecordId = 0,
                    RecordDate = DateTime.Now,
                    BiometricData = dto.EmployeeBiometricDtos.FirstOrDefault().BiometricData,
                };

                var _employmentDetails = new EmploymentDetail()
                {
                    EmploymentId = 0,
                    HireDate = dto.EmploymentDetailDtos.FirstOrDefault().HireDate,
                    PositionId = dto.EmploymentDetailDtos.FirstOrDefault().PositionId,
                    PayRate = dto.EmploymentDetailDtos.FirstOrDefault().PayRate,
                    IncomeTaxRate = dto.EmploymentDetailDtos.FirstOrDefault().IncomeTaxRate,
                    PagibigEmployeeRate = dto.EmploymentDetailDtos.FirstOrDefault().PagibigEmployeeRate,
                    PhilhealthEmployeeRate = dto.EmploymentDetailDtos.FirstOrDefault().PhilhealthEmployeeRate,
                    SssEmployeeRate = dto.EmploymentDetailDtos.FirstOrDefault().SssEmployeeRate,
                    Status = dto.EmploymentDetailDtos.FirstOrDefault().Status
                };

                _personalInfo.ContactInformations = (ICollection<ContactInformation>)_contactInfo;
                _personalInfo.EmployeeBiometrics = (ICollection<EmployeeBiometric>)_employeeBio;
                _personalInfo.EmploymentDetails = (ICollection<EmploymentDetail>)_employmentDetails;

                await _context.AddAsync(_personalInfo);
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
