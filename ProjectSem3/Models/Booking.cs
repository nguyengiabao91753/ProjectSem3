using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? UserId { get; set; }

    public int? BusTripId { get; set; }

    public DateTime BookingDate { get; set; }

    public decimal? Total { get; set; }

    public byte? PaymentStatus { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual BusesTrip? BusTrip { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
