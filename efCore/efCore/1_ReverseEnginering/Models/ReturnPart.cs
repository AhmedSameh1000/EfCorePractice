using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class ReturnPart
{
    public int Id { get; set; }

    public int ReturnOrderId { get; set; }

    public int Quantity { get; set; }

    public string PartName { get; set; } = null!;

    public decimal PartPrice { get; set; }

    public virtual ReturnOrder ReturnOrder { get; set; } = null!;
}
