namespace ProjectSem3.DTOs
{
    public class TripDTO
    {
        public int TripId { get; set; }
        //[Required(ErrorMessage = "Departure Location ID is required.")]
        public int? DepartureLocationId { get; set; }

        //[Required(ErrorMessage = "Arrival Location ID is required.")]
        public int? ArrivalLocationId { get; set; }

        //[Required(ErrorMessage = "Date Start is required.")]
        public string? DateStart { get; set; }

        //[Required(ErrorMessage = "Date End is required.")]
        public string? DateEnd { get; set; }

        //[Range(0, 1, ErrorMessage = "Status must be 0 or 1.")]
        public byte? Status { get; set; }
    }
}


