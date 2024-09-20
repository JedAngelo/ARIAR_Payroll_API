using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class PostionDto
    {
        public int PositionId { get; set; }

        public string PositionName { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual List<EmploymentDetail> EmploymentDetails { get; set; } = new List<EmploymentDetail>();
    }
}
