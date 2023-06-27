using System;
using System.Collections.Generic;

namespace THERIDEURENT1.models;

public partial class Car
{
    public string CarNo { get; set; } = null!;

    public int? CarMakeId { get; set; }

    public string? Model { get; set; }

    public int? CarBodyTypeId { get; set; }

    public int? KilometersTravelled { get; set; }

    public int? ServiceKilometers { get; set; }

    public string? Available { get; set; }

    public virtual CarBodyType? CarBodyType { get; set; }

    public virtual CarMake? CarMake { get; set; }

    public virtual ICollection<CarReturn> CarReturns { get; set; } = new List<CarReturn>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
