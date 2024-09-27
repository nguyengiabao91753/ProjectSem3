namespace ProjectSem3.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? UserId { get; set; }

    public int? BusTripId { get; set; }

    public int? SeatId { get; set; }

    public int? AgeGroupId { get; set; }

    public decimal Distance { get; set; }

    public decimal? PriceAfterDiscount { get; set; }

    public DateTime BookingDate { get; set; }

    public string TicketCode { get; set; } = null!;

    public byte? TicketStatus { get; set; }

    public byte? PaymentStatus { get; set; }

    public virtual AgeGroup? AgeGroup { get; set; }

    public virtual BusesTrip? BusTrip { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
