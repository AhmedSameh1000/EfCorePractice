using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool? IsMain { get; set; }

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
}
