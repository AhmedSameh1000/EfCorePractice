using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Test
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool IsActive { get; set; }
}
