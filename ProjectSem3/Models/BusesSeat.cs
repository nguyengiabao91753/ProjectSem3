using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class BusesSeat
{
    public int SeatId { get; set; }

    public int? BusId { get; set; }

    public string Name { get; set; } = null!;

    public byte? Status { get; set; }

    public virtual Bus? Bus { get; set; }
}
