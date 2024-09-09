using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Trip
{
    public int TripId { get; set; }

    public int? DepartureLocationId { get; set; }

    public int? ArrivalLocationId { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public byte? Status { get; set; }

    public virtual Location? ArrivalLocation { get; set; }

    public virtual ICollection<BusesTrip> BusesTrips { get; set; } = new List<BusesTrip>();

    public virtual Location? DepartureLocation { get; set; }
}
