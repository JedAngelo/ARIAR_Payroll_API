using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class BackupDto
    {
        public int BackupId { get; set; }

        public DateTime BackupDate { get; set; }

        public string FileLocation { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
    }
}
