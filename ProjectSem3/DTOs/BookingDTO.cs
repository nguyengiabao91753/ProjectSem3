using ProjectSem3.Models;

namespace ProjectSem3.DTOs;

public class BookingDTO
{
    public int BookingId { get; set; }

    public string FullName { get; set; } = null!;

    public string BirthDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? UserId { get; set; }

    public int? BusTripId { get; set; }

    public int? SeatId { get; set; }

    public int? AgeGroupId { get; set; }

    public decimal Distance { get; set; }

    public decimal? PriceAfterDiscount { get; set; }

    public string? BookingDate { get; set; }

    public string TicketCode { get; set; } = null!;

    public byte? TicketStatus { get; set; }

    public byte? PaymentStatus { get; set; }

    
}
