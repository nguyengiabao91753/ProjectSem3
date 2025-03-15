using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public byte? Status { get; set; }

    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    public virtual ICollection<Trip> TripArrivalLocations { get; set; } = new List<Trip>();

    public virtual ICollection<Trip> TripDepartureLocations { get; set; } = new List<Trip>();
}
