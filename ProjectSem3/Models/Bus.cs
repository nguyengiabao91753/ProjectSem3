using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Bus
{
    public int BusId { get; set; }

    public int? BusTypeId { get; set; }

    public byte? AirConditioned { get; set; }

    public string LicensePlate { get; set; } = null!;

    public int SeatCount { get; set; }

    public decimal BasePrice { get; set; }

    public virtual BusType? BusType { get; set; }

    public virtual ICollection<BusesSeat> BusesSeats { get; set; } = new List<BusesSeat>();

    public virtual ICollection<BusesTrip> BusesTrips { get; set; } = new List<BusesTrip>();
}
