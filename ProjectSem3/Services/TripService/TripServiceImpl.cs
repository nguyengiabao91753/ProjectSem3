using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.TripService;

public class TripServiceImpl : TripService
{
    private DatabaseContext db;
    private IMapper mapper;
    public TripServiceImpl(
        DatabaseContext databaseContext,
        IMapper mapper
        )

    {
        this.db = databaseContext;
        this.mapper = mapper;
    }

    public bool Create(TripDTO tripDTO)
    {
        try
        {
            // Map từ DTO sang Trip
            var trip = mapper.Map<Trip>(tripDTO);

            // Kiểm tra các trường cần thiết
            if (trip.DepartureLocationId == null || trip.ArrivalLocationId == null || trip.DateStart == null || trip.DateEnd == null || trip.Status == null)
            {
                throw new Exception("All required fields must be provided.");
            }

            db.Trips.Add(trip);
            return db.SaveChanges() > 0;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Database error: " + ex.InnerException?.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating trip: " + ex.Message);
            return false;
        }
    }
    public TripDTO findById(int id)
    {
        return mapper.Map<TripDTO>(db.Trips.Find(id));
    }



    public bool Delete(int id)
    {
        var trip = db.Trips.SingleOrDefault(a => a.TripId == id);
        if (trip != null)
        {
            db.Trips.Remove(trip);
            return db.SaveChanges() > 0; // Trả về true nếu xóa thành công
        }
        return false; // Trả về false nếu không tìm thấy trip
    }

    public List<TripDTO> GetAll()
    {
        return mapper.Map<List<TripDTO>>(db.Trips.OrderByDescending(b => b.TripId).ToList());
    }
    public bool Update(TripDTO tripDTO)
    {
        try
        {
            var trip = mapper.Map<Trip>(tripDTO);
            db.Entry(trip).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        (Exception ex)
        {
            return false;
        }
    }
}
