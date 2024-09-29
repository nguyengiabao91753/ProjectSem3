using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BookingService;

public class BookingServiceImpl : BookingService
{
    private DatabaseContext db;
    private IMapper mapper;
    public BookingServiceImpl(
        DatabaseContext databaseContext,
        IMapper mappern

        )
    {
        db = databaseContext;
        this.mapper = mapper;
    }

    public string GenerateTicketCode(int plusId)
    {
        var id = "";
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();

        for (int i = 0; i < 5; i++)
        {
            id += chars[random.Next(chars.Length)] + plusId;
        }
        return id;
    }

    public bool Create(BookingDTO bookingDTO, List<BookingDetail> bookingDetails)
    {
        try
        {
            var book = mapper.Map<Booking>(bookingDTO);
            var total = bookingDetails.Sum(d => d.PriceAfterDiscount);
            book.Total = (decimal)total;
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
                    return db.SaveChanges() > 0;

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
}