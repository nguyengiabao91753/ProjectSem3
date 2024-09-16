using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;
using System;

namespace ProjectSem3.Services.BookingService;

public class BookingServiceImpl : BookingService
{
    private DatabaseContext db;
    private IMapper mapper;
    public BookingServiceImpl(
        DatabaseContext databaseContext,
        IMapper mapper
        )
    {
        db = databaseContext;
        this.mapper = mapper;
    }

    public bool Create(BookingDTO bookingDTO)
    {
        try
        {
            var book = mapper.Map<Booking>(bookingDTO);
            var bustrip = db.BusesTrips.FirstOrDefault(b => b.BusTripId == book.BusTripId);
            var seat = db.BusesSeats.FirstOrDefault(s => s.BusId == bustrip.BusId && s.SeatId == book.SeatId);
            if(seat != null)
            {
                seat.Status = 0;
                db.BusesSeats.Update(seat);
                var countseat = db.BusesSeats.Count(s => s.BusId == bustrip.BusId && s.Status == 1);
                if (countseat == 0)
                {
                    bustrip.Status = 3;
                    db.BusesTrips.Update(bustrip);

                }
            }
            

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Update(BookingDTO bookingDTO)
    {
        throw new NotImplementedException();
    }
}
