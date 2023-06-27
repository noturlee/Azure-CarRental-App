using System;
using System.Collections.Generic;

namespace THERIDEURENT1.models;

public partial class Rental
{
    public int RentalId { get; set; }

    public string CarNo { get; set; } = null!;

    public string InspectorNo { get; set; } = null!;

    public int DriverId { get; set; }

    public decimal RentalFee { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Car CarNoNavigation { get; set; } = null!;

    public virtual ICollection<CarReturn> CarReturns { get; set; } = new List<CarReturn>();

    public virtual Driver Driver { get; set; } = null!;

    public virtual Inspector InspectorNoNavigation { get; set; } = null!;
}
