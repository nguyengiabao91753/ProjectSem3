using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BookingService;

public interface BookingService
{
    public bool Create(BookingDTO bookingDTO, List<BookingDetailDTO> bookingDetailsdto, string paymentMethod);

    public bool Update(BookingDTO bookingDTO);

    public bool Cancel(BookingDTO bookingDTO);

    public List<BookingDTO> GetAll();

    public List<BookingDTO> GetAllByUserId(int id);

    public List<BookingDetailDTO> GetDetailByBooking(int id);
    public List<BookingDetailDTO> GetAllDetail();
}