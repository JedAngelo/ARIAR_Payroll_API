using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class PersonalInformationDto
    {
        public Guid PersonalId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = null!;

        public byte Age { get; set; }

        public DateOnly CreatedDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateOnly? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateOnly? DeletedDate { get; set; }

        public string? DeletedBy { get; set; }

        public virtual ICollection<AttendanceDto> AttendanceDtos { get; set; } = new List<AttendanceDto>();

        public virtual ICollection<ContactInformationDto> ContactInformationDtos { get; set; } = new List<ContactInformationDto>();

        public virtual ICollection<EmployeeBiometricDto> EmployeeBiometricDtos { get; set; } = new List<EmployeeBiometricDto>();

        public virtual ICollection<EmploymentDetailDto> EmploymentDetailDtos { get; set; } = new List<EmploymentDetailDto>();

        public virtual ICollection<LeaveDto> LeaveDtos { get; set; } = new List<LeaveDto>();

        public virtual ICollection<PayrollDto> PayrollDtos { get; set; } = new List<PayrollDto>();
    }
}
