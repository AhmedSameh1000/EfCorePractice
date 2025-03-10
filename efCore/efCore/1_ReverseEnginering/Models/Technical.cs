using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Technical
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
