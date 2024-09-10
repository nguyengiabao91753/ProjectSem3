using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class AgeGroup
{
    public int AgeGroupId { get; set; }

    public string Name { get; set; } = null!;

    public string? Discount { get; set; }

    public byte? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
