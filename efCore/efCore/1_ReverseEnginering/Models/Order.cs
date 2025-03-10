using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CustomerId { get; set; }

    public int StoreId { get; set; }

    public int OrderTypeId { get; set; }

    public int TechnicalId { get; set; }

    public string Description { get; set; } = null!;

    public int OrderStatus { get; set; }

    public string? AppUserId { get; set; }

    public virtual AspNetUser? AppUser { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderImage> OrderImages { get; set; } = new List<OrderImage>();

    public virtual OrderType OrderType { get; set; } = null!;

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual Store Store { get; set; } = null!;

    public virtual Technical Technical { get; set; } = null!;
}
