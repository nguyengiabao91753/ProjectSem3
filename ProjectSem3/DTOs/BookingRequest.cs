namespace ProjectSem3.DTOs;

public class BookingRequest
{
    public BookingDTO BookingDTO { get; set; }
    public List<BookingDetailDTO> BookingDetailDTOs { get; set; }
}
