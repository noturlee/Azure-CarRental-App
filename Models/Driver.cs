using System;
using System.Collections.Generic;

namespace THERIDEURENT1.models;

public partial class Driver
{
    public int DriverId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public virtual ICollection<CarReturn> CarReturns { get; set; } = new List<CarReturn>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
