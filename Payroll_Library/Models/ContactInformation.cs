using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class ContactInformation
{
    [Key]
    public int ContactId { get; set; }

    public Guid PersonalId { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual PersonalInformation Personnel { get; set; } = null!;
}
