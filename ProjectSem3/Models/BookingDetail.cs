namespace ProjectSem3.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int? BookingId { get; set; }

    public int? SeatId { get; set; }

    public string? SeatName { get; set; }

    public int? AgeGroupId { get; set; }

    public decimal? PriceAfterDiscount { get; set; }

    public string TicketCode { get; set; } = null!;

    public byte? TicketStatus { get; set; }

    public virtual AgeGroup? AgeGroup { get; set; }

    public virtual Booking? Booking { get; set; }
}