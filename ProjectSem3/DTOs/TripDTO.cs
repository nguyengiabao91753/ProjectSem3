namespace ProjectSem3.DTOs
{
    public class TripDTO
    {
        public int TripId { get; set; }
        public int? DepartureLocationId { get; set; }

        public int? ArrivalLocationId { get; set; }

        public string? DateStart { get; set; }

        public string? DateEnd { get; set; }

        public byte? Status { get; set; }
        public string DepartureLocationName { get; set; }
        public string ArrivalLocationName { get; set; }
    }
}


