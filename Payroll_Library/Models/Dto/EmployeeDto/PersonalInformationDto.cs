using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.EmployeeDto
{
    public class PersonalInformationDto
    {
        public Guid? PersonalId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;

        public byte Age { get; set; }

        public byte[]? EmployeeImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public bool IsActive { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }


        public virtual ContactInformationDto ContactInformationDtos { get; set; } = new ContactInformationDto();

        //public virtual List<EmployeeBiometricDto> EmployeeBiometricDtos { get; set; } = new List<EmployeeBiometricDto>();

        public virtual EmploymentDetailDto EmploymentDetailDtos { get; set; } = new EmploymentDetailDto();


    }
}
