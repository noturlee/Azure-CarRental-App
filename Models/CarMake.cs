using System;
using System.Collections.Generic;

namespace THERIDEURENT1.models;

public partial class CarMake
{
    public int CarMakeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
