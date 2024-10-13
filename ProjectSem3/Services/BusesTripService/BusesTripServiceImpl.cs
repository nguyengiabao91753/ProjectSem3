using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BusesTripService;

public class BusesTripServiceImpl : BusesTripService
{
    private DatabaseContext db;
    private IMapper mapper;
    public BusesTripServiceImpl(DatabaseContext database, IMapper mapper)
    {
        db = database;
        this.mapper = mapper;
    }
    public bool Create(BusesTripDTO busesTripDTO)
    {
        try
        {
            var bt = mapper.Map<BusesTrip>(busesTripDTO);
            db.BusesTrips.Add(bt);
            var bus =db.Buses.Find(bt.BusId);
            if (bus != null)
            {
                bus.Status = 2;
                db.Update(bus);
            }
            return db.SaveChanges() > 0;

        }catch (Exception ex)
        {
            return false;
        }
    }

    public bool Done(BusesTripDTO busesTripDTO)
    {
        try
        {
            var bt = mapper.Map<BusesTrip>(busesTripDTO);
            var bus = db.Buses.Find(bt.BusId);
            if (bus != null)
            {
                bus.Status = 1;
                db.Update(bus);
            }
            db.Entry(bt).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<BusesTripDTO> GetAll()
    {
        

        return mapper.Map<List<BusesTripDTO>>(db.BusesTrips.Where(b=>b.Status!=0).OrderByDescending(b=>b.BusTripId).ToList());
    }

    public List<BusesTripDTO> GetAllActive()
    {
        var current = DateTime.Now;

        return mapper.Map<List<BusesTripDTO>>(db.BusesTrips.Where(b => b.Status != 0 && b.Trip.DateStart > current).OrderByDescending(b => b.BusTripId).ToList());
    }

    public BusesTripDTO GetById(int id)
    {
       return mapper.Map<BusesTripDTO>(db.BusesTrips.Find(id));
    }

    public bool Update(BusesTripDTO busesTripDTO)
    {
        try
        {
            var bt = mapper.Map<BusesTrip>(busesTripDTO);
            db.Entry(bt).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }catch(Exception ex)
        {
            return false;
        }
    }
}
