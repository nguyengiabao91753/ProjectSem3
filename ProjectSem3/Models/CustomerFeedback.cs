using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class CustomerFeedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public int? BusTripId { get; set; }

    public string? Content { get; set; }

    public DateTime FeedbackDate { get; set; }

    public virtual BusesTrip? BusTrip { get; set; }

    public virtual User? User { get; set; }
}
