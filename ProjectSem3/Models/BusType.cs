using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class BusType
{
    public int BusTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();
}
