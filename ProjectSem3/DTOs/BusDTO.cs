namespace ProjectSem3.DTOs;

public class BusDTO
{
    public int BusId { get; set; }

    public int BusTypeId { get; set; }

    public byte AirConditioned { get; set; }

    public string LicensePlate { get; set; } = null!;

    public int SeatCount { get; set; }

    public decimal BasePrice { get; set; }

    public byte Status { get; set; }
    public string BusName { get; set; }

    public int? LocationId { get; set; }
    public string LocationName { get; set; }
}
