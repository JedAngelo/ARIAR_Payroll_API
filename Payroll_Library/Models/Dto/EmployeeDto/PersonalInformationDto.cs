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

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = null!;

        public byte Age { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; } = null!;



        public bool IsActive { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }


        public virtual List<ContactInformationDto> ContactInformationDtos { get; set; } = new List<ContactInformationDto>();

        //public virtual List<EmployeeBiometricDto> EmployeeBiometricDtos { get; set; } = new List<EmployeeBiometricDto>();

        public virtual List<EmploymentDetailDto> EmploymentDetailDtos { get; set; } = new List<EmploymentDetailDto>();


    }
}
