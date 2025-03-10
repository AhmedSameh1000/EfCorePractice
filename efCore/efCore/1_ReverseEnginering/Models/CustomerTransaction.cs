using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class CustomerTransaction
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string AppUserId { get; set; } = null!;

    public int TransactionType { get; set; }

    public string Description { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual AspNetUser AppUser { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
