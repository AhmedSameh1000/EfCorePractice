using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class ReturnOrder
{
    public int Id { get; set; }

    public DateTime ReturnDate { get; set; }

    public decimal RefundAmount { get; set; }

    public string? Reason { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<ReturnPart> ReturnParts { get; set; } = new List<ReturnPart>();
}
