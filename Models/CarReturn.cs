using System;
using System.Collections.Generic;

namespace THERIDEURENT1.models;

public partial class CarReturn
{
    public int ReturnId { get; set; }

    public string? CarNo { get; set; }

    public int? RentalId { get; set; }

    public string? InspectorNo { get; set; }

    public int? DriverId { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? ElapsedDays { get; set; }

    public int? Fine { get; set; }

    public virtual Car? CarNoNavigation { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Inspector? InspectorNoNavigation { get; set; }

    public virtual Rental? Rental { get; set; }
}
