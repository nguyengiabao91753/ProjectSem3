using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class BusesTrip
{
    public int BusTripId { get; set; }

    public int? BusId { get; set; }

    public int? TripId { get; set; }

    public decimal? Price { get; set; }

    public byte? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Bus? Bus { get; set; }

    public virtual ICollection<CustomerFeedback> CustomerFeedbacks { get; set; } = new List<CustomerFeedback>();

    public virtual Trip? Trip { get; set; }
}
