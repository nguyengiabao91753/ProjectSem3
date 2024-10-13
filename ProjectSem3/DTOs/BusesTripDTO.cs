namespace ProjectSem3.DTOs;

public class BusesTripDTO
{
    public int BusTripId { get; set; }

    public int BusId { get; set; }

    //Bus
    public string? BusTypeName { get; set; }

    public byte AirConditioned { get; set; }

    public int SeatCount { get; set; }

    public string? LicensePlate { get; set; }   
    //.

    public int TripId { get; set; }

    //Trip
    public string? DepartureLocationName { get; set; }
  
    public string? ArrivalLocationName { get; set; }

    public string? DateStart { get; set; }

    public string? DateEnd { get; set; }
    //.

    public string? Price { get; set; }

    public byte? Status { get; set; }
}
