using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BusService;

public class BusServiceImpl : BusService
{
    private readonly DatabaseContext db;
    private readonly IMapper mapper;

    public BusServiceImpl(DatabaseContext _db, IMapper _mapper)
    {
        db = _db;
        mapper = _mapper;
    }

    public bool Create(BusDTO busDTO)
    {
        try
        {
            var bus = mapper.Map<Bus>(busDTO);
            db.Buses.Add(bus);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool Disable(int id)
    {
        var bus = db.Buses.SingleOrDefault(b => b.BusId == id);
        if (bus != null)
        {
            bus.Status = 0;
            return db.SaveChanges() > 0;
        }
        var busDTO = mapper.Map<BusDTO>(bus);
        return Update(busDTO);
    }

    public List<BusDTO> GetAll()
    {
        return mapper.Map<List<BusDTO>>(db.Buses.ToList());
    }

    public bool Update(BusDTO busDTO)
    {
        try
        {
            var bus = mapper.Map<Bus>(busDTO);
            db.Entry(bus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
