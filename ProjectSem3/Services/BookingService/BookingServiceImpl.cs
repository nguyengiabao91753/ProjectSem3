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
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();

        for (int i = 0; i < 5; i++)
        {
            id += chars[random.Next(chars.Length)] + plusId;
        }
        return id;
    }

    public bool Create(BookingDTO bookingDTO)
    {
        try
        {
            var book = mapper.Map<Booking>(bookingDTO);
            var bustrip = db.BusesTrips.FirstOrDefault(b => b.BusTripId == book.BusTripId);
            if (bustrip != null)
            {
                //var seat = db.BusesSeats.FirstOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == book.SeatId);

                //if (seat != null)
                //{
                //    seat.Status = 0;
                //    db.BusesSeats.Update(seat);
                //    var countseat = db.BusesSeats.Count(s => s.BusId == bustrip.BusId && s.Status == 1);
                //    if (countseat == 0)
                //    {
                //        bustrip.Status = 2;
                //        db.BusesTrips.Update(bustrip);

                //    }
                //    book.BookingDate = DateTime.Now;

                //    db.Bookings.Add(book);
                //    if( db.SaveChanges() > 0)
                //    {
                //        book.TicketCode = GenerateTicketCode(book.BookingId);
                //        db.Bookings.Update(book);
                //        return db.SaveChanges() > 0;
                //    }



                //}
                /*                var seat = db.BusesSeats.FirstOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == book.SeatId);*/

                /*                if (seat != null)
                                {
                                    seat.Status = 0;
                                    db.BusesSeats.Update(seat);
                                    var countseat = db.BusesSeats.Count(s => s.BusId == bustrip.BusId && s.Status == 1);
                                    if (countseat == 0)
                                    {
                                        bustrip.Status = 2;
                                        db.BusesTrips.Update(bustrip);

                                    }
                                    book.BookingDate = DateTime.Now;

                                    db.Bookings.Add(book);
                                    if (db.SaveChanges() > 0)
                                    {
                *//*                        book.TicketCode = GenerateTicketCode(book.BookingId);
                                        db.Bookings.Update(book);*//*
                                        return db.SaveChanges() > 0;
                                    }



                                }*/

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
            if (bustrip != null)
            {
                //var seat = db.BusesSeats.FirstOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == book.SeatId);
                //db.BusesSeats.Update(seat);
                //if (seat != null)
                //{
                //    seat.Status = 1;
                //    var countseat = db.BusesSeats.Count(b=>b.BusId==bustrip.BusId && b.SeatId==book.SeatId);
                //    if(countseat == 0)
                //    {
                //        bustrip.Status = 1;
                //        db.BusesTrips.Update(bustrip);
                //    }

                //    book.TicketStatus = 2;
                //    book.TicketCode = "";
                //    db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //    return db.SaveChanges() > 0;

                //}

                /*           var seat = db.BusesSeats.FirstOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == book.SeatId);*/
                /*                db.BusesSeats.Update(seat);
                                if (seat != null)
                                {*/
                /*                    seat.Status = 1;*/
                /*                    var countseat = db.BusesSeats.Count(b => b.BusId == bustrip.BusId && b.SeatId == book.SeatId);*/
                /*                    if (countseat == 0)
                                    {
                                        bustrip.Status = 1;
                                        db.BusesTrips.Update(bustrip);
                                    }*/

                /*                    book.TicketStatus = 2;
                                    book.TicketCode = "";*/
                db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return db.SaveChanges() > 0;
                /*
                                }*/

            }

            return false;

        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
