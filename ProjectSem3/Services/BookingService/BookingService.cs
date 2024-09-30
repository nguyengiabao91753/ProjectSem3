using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BookingService;

public interface BookingService
{
    public BookingDTO GetBookingById(int bookingId);

    public bool Create(BookingDTO bookingDTO, List<BookingDetailDTO> bookingDetailsdto);

    public bool Update(BookingDTO bookingDTO);

    public bool Cancel(BookingDTO bookingDTO);
}