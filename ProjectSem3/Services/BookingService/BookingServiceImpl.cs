using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Helpers;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BookingService;

public class BookingServiceImpl : BookingService
{
    private DatabaseContext db;
    private IMapper mapper;
    private IConfiguration configuration;
    public BookingServiceImpl(
        DatabaseContext databaseContext,
        IMapper mapper,
        IConfiguration configuration

        )
    {
        db = databaseContext;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public string GenerateTicketCode(int plusId)
    {
        var id = "";
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();

        for (int i = 0; i < 5; i++)
        {
            id += chars[random.Next(chars.Length)];
        }
        return id + plusId;
    }

    public bool Create(BookingDTO bookingDTO, List<BookingDetailDTO> bookingDetailsdto, string paymentMethod)
    {
        try
        {
            var book = mapper.Map<Booking>(bookingDTO);
            var bookingDetails = mapper.Map<List<BookingDetail>>(bookingDetailsdto);
            var total = bookingDetails.Sum(d => d.PriceAfterDiscount);
            book.Total = total;
            book.BookingDate = DateTime.Now;
            db.Bookings.Add(book);
            if (db.SaveChanges() > 0)
            {
                var bustrip = db.BusesTrips.SingleOrDefault(b => b.BusTripId == book.BusTripId);

                if (bustrip != null)
                {
                    for (int i = 0; i < bookingDetails.Count; i++)
                    {

                        var seat = db.BusesSeats.FirstOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == bookingDetails[i].SeatId);

                        if (seat != null)
                        {
                            seat.Status = 0;
                            db.BusesSeats.Update(seat);

                            bookingDetails[i].BookingId = book.BookingId;
                            bookingDetails[i].TicketStatus = 1;
                            db.BookingDetails.Add(bookingDetails[i]);

                            if (db.SaveChanges() > 0)
                            {
                                bookingDetails[i].TicketCode = GenerateTicketCode(bookingDetails[i].BookingDetailId);
                                db.BookingDetails.Update(bookingDetails[i]);
                                db.SaveChanges();
                            }



                        }
                    }
                    var countseat = db.BusesSeats.Count(s => s.BusId == bustrip.BusId && s.Status == 1);
                    if (countseat == 0)
                    {
                        bustrip.Status = 2;
                        db.BusesTrips.Update(bustrip);

                    }
                    var payment = new Models.Payment();
                    payment.BookingId = book.BookingId;
                    payment.PaymentDate = DateTime.Now;
                    payment.Amount = book.Total;
                    payment.PaymentMethod = paymentMethod;
                    book.PaymentStatus = 1;

                    db.Bookings.Update(book);
                    db.Payments.Add(payment);

                    if (db.SaveChanges() > 0)
                    {
                        SenMailwithQRCodeHelper senMailwithQRCode = new SenMailwithQRCodeHelper(configuration);
                        senMailwithQRCode.SenMailwithQRCode(book, bookingDetails);
                        return true;

                    }

                }
            }

            return false;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Update(BookingDTO bookingDTO)
    {
        try
        {
            var book = mapper.Map<Booking>(bookingDTO);
            db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Cancel(BookingDTO bookingDTO)
    {
        try
        {
            var book = mapper.Map<Booking>(bookingDTO);
            var bustrip = db.BusesTrips.FirstOrDefault(b => b.BusTripId == book.BusTripId);
            var bookingdetails = db.BookingDetails.Where(b => b.BookingId == book.BookingId).ToList();
            if (bustrip != null && bookingdetails != null)
            {
                for (int i = 0; i < bookingdetails.Count; i++)
                {


                    var seat = db.BusesSeats.SingleOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == bookingdetails[i].SeatId);

                    if (seat != null)
                    {
                        seat.Status = 1;
                        db.BusesSeats.Update(seat);


                        bookingdetails[i].TicketStatus = 2;
                        bookingdetails[i].TicketCode = "";
                        db.Entry(bookingdetails[i]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                var countseat = db.BusesSeats.Count(b => b.BusId == bustrip.BusId && b.Status == 1);
                if (countseat != 0)
                {
                    bustrip.Status = 1;
                    db.BusesTrips.Update(bustrip);
                }
                return db.SaveChanges() > 0;

            }

            return false;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<BookingDTO> GetAll()
    {
        return mapper.Map<List<BookingDTO>>(db.Bookings.OrderByDescending(b => b.BookingId).ToList());
    }

    public List<BookingDetailDTO> GetAllDetail()
    {
        return mapper.Map<List<BookingDetailDTO>>(db.BookingDetails.ToList());
    }

    public List<BookingDTO> GetAllByUserId(int id)
    {
        return mapper.Map<List<BookingDTO>>(db.Bookings.Where(b => b.UserId == id).OrderByDescending(b => b.BookingId).ToList());
    }

    public List<BookingDetailDTO> GetDetailByBooking(int id)
    {
        return mapper.Map<List<BookingDetailDTO>>(db.BookingDetails.Where(d => d.BookingId == id).ToList());
    }

    public BookingDetailDTO GetBookingDetailByTicketCode(string ticketCode)
    {
        var detail = mapper.Map<BookingDetailDTO>(db.BookingDetails.Where(d=>d.TicketCode == ticketCode).FirstOrDefault());
        return detail;
    }

    public BookingDTO GetBookingById(int id)
    {
        var booking = mapper.Map<BookingDTO>(db.Bookings.Find(id));
        return booking;
    }

    public BookingDetail getBookingDetailByTicketCode(string ticketCode)
    {
        var booking = db.BookingDetails.Where(d => d.TicketCode == ticketCode).FirstOrDefault();
        return booking;
    }

    public Booking getBookingById(int id)
    {
        var booking = db.Bookings.Find(id);
        return booking;
    }

    public bool UseTicket(string ticketCode)
    {
       
        var detail = db.BookingDetails.Where(d => d.TicketCode == ticketCode).FirstOrDefault();
        detail.TicketStatus = 0;
        db.BookingDetails.Update(detail);
        return db.SaveChanges() > 0;
    }
}