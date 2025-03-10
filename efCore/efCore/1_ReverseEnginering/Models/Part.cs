using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Part
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }

    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
