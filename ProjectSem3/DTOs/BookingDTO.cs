using ProjectSem3.Models;

namespace ProjectSem3.DTOs;

public class BookingDTO
{
    public int BookingId { get; set; }

    public string FullName { get; set; } = null!;


    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }



    public int? UserId { get; set; }

    public int? BusTripId { get; set; }
    public string? BookingDate { get; set; }

    public decimal Total { get; set; }
    public byte? PaymentStatus { get; set; }

    
}
