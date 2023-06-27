using System;
using System.Collections.Generic;

namespace THERIDEURENT1.models;

public partial class CarBodyType
{
    public int CarBodyTypeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
