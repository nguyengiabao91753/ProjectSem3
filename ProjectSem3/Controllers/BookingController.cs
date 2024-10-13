using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.BookingService;

namespace ProjectSem3.Controllers;
[Route("api/booking")]
[ApiController]
public class BookingController : Controller
{
    private BookingService bookingService;
    public BookingController(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("create")]
    public IActionResult Create([FromBody] BookingRequest bookingRequest)
    {
        try
        {
            bool result = bookingService.Create(bookingRequest.BookingDTO, bookingRequest.BookingDetailDTOs, "Paypal");

            return Ok(new
            {
                status = result
            }
            );

            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("getall")]
    public IActionResult Getall()
    {
        try
        {
            var booking = bookingService.GetAll();
            var details = bookingService.GetAllDetail();

            return Ok(new
            {
                booking = booking,
                details = details
            }
            );

            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("getbookingrequestbyticketcode/{ticketcode}")]
    public IActionResult GetBookingRequestByTicketCode(string ticketcode)
    {
        try
        {
            var detail = bookingService.getBookingDetailByTicketCode(ticketcode);
            
            if (detail == null)
            {
                return Ok(new
                {
                    status = false
                });
            }
            var booking = bookingService.getBookingById(detail.BookingId);
            return Ok(new
            {
                status = true,
                departure = booking.BusTrip.Trip.DepartureLocation.Name,
                arrival = booking.BusTrip.Trip.ArrivalLocation.Name,
                dateStart = booking.BusTrip.Trip.DateStart.ToString("HH:mm:ss dd/MM/yyyy"),
                dateEnd = booking.BusTrip.Trip.DateEnd.ToString("HH:mm:ss dd/MM/yyyy"),
                fullName = booking.FullName,
                email = booking.Email,
                seatName = detail.SeatName,
                busTypeName = booking.BusTrip.Bus.BusType.Name,
                licensePlate = booking.BusTrip.Bus.LicensePlate,
                ticketCode = detail.TicketCode,
                bookingDate = booking.BookingDate.ToString("HH:mm:ss dd/MM/yyyy"),
                ticketStatus = detail.TicketStatus,
            }
            );

            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("useticket/{ticketcode}")]
    public IActionResult UseTicket(string ticketcode)
    {
        try
        {
            var result = bookingService.UseTicket(ticketcode);


            return Ok(new
            {
                status = result
            });



            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("getbookingbyemail/{email}")]
    public IActionResult GetBookingByEmail(string email)
    {
        try
        {
            var bookings = bookingService.GetAllByEmail(email);
            var detail = bookingService.GetAllDetail();
            if (bookings == null)
            {
                return Ok(new
                {
                    status = false
                });
            }
            return Ok(new
            {
                status = true,
                bookings = bookings,
                data = detail
            }
            );

            throw new Exception();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}