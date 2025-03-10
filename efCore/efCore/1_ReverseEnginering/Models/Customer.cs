using System;
using System.Collections.Generic;

namespace _1_ReverseEnginering.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CustomerTransaction> CustomerTransactions { get; set; } = new List<CustomerTransaction>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual ICollection<ReturnOrder> ReturnOrders { get; set; } = new List<ReturnOrder>();

    public virtual ICollection<Statement> Statements { get; set; } = new List<Statement>();
}
