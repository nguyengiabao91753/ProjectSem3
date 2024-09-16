namespace ProjectSem3.Models;

public partial class BusType
{
    public int BusTypeId { get; set; }

    public string Name { get; set; } = null!;

    public byte? Status { get; set; }

    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();
}
