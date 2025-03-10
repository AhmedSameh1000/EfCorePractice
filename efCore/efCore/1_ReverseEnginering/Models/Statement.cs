using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Statement
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime StatmentDate { get; set; }

    public string Note { get; set; } = null!;

    public int StatmentType { get; set; }

    public decimal? Ammount { get; set; }

    public decimal? NewBalance { get; set; }

    public int? OrderId { get; set; }

    public string? UserId { get; set; }

    public int? Discount { get; set; }

    public int? SourceType { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual AspNetUser? User { get; set; }
}
