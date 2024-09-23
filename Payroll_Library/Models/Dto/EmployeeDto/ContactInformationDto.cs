using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.EmployeeDto
{
    public class ContactInformationDto
    {
        public int ContactId { get; set; }

        public Guid PersonalId { get; set; }

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        //public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;
    }
}
