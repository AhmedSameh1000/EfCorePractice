using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class OrderImage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public int PhotoType { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
