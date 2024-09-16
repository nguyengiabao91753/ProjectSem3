using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BookingService;

public interface BookingService
{
    public bool Create(BookingDTO bookingDTO);

    public bool Update(BookingDTO bookingDTO);
}
