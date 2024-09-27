namespace ProjectSem3.DTOs;

public class PaymentDTO
{
    public int PaymentId { get; set; }

    public int? BookingId { get; set; }

    public string? PaymentDate { get; set; }

    public decimal Amount { get; set; }

    //public string? PaymentMethod { get; set; }

    //public string? FullName { get; set; }

    //public string? BirthDate { get; set; }

    //public string? PhoneNumber { get; set; }

}
