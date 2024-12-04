using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class Backup
{
    public int BackupId { get; set; }

    public DateTime BackupDate { get; set; }

    public string FileLocation { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
