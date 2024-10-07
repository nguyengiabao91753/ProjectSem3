namespace ProjectSem3.DTOs;

public class BookingDetailDTO
{
    public int BookingDetailId { get; set; }

    public int BookingId { get; set; }

    public int? SeatId { get; set; }

    public string? SeatName { get; set; }

    public int? AgeGroupId { get; set; }

    public string? AgeGroupName { get; set; }

    public decimal PriceAfterDiscount { get; set; }

    public string TicketCode { get; set; } = null!;

    public byte? TicketStatus { get; set; }
}
