namespace ProjectSem3.DTOs;

public class BusesSeatDTO
{
    public int SeatId { get; set; }

    public int BusId { get; set; }

    public string Name { get; set; }

    public byte Status { get; set; }

    public string BusLicensePlate { get; set; } = null!;
}
