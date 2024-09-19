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

    public bool checkLicensePlateExists(string licensePlate)
    {
        return db.Buses.Any(b => b.LicensePlate == licensePlate);
    }

    public bool Create(BusDTO busDTO)
    {
        try
        {
            var bus = mapper.Map<Bus>(busDTO);
            var busType = db.BusTypes.FirstOrDefault(b => b.Name == busDTO.BusName);
            if (busType != null)
            {
                if (busDTO.BusTypeId != null && busDTO.BusTypeId != busType.BusTypeId)
                {
                    throw new InvalidOperationException("BusTypeId does not match the BusName provided.");
                }

                bus.BusTypeId = busType.BusTypeId;
            }
            db.Buses.Add(bus);
            if (db.SaveChanges() > 0)
            {
                for (int i = 1; i <= bus.SeatCount; i++)
                {
                    BusesSeat seat = new BusesSeat();
                    seat.BusId = bus.BusId;
                    seat.Name = i.ToString();
                    db.BusesSeats.Add(seat);
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

    public BusDTO findById(int id)
    {
        return mapper.Map<BusDTO>(db.Buses.Find(id));
    }

    public List<BusDTO> GetAll()
    {
        return mapper.Map<List<BusDTO>>(db.Buses.OrderByDescending(b => b.BusId).ToList());
    }

    public bool Update(BusDTO busDTO)
    {
        try
        {
            var bus = mapper.Map<Bus>(busDTO);
            var busType = db.BusTypes.FirstOrDefault(b => b.Name == busDTO.BusName);
            if (busType != null)
            {
                if (busDTO.BusTypeId != null && busDTO.BusTypeId != busType.BusTypeId)
                {
                    throw new InvalidOperationException("BusTypeId does not match the BusName provided.");
                }

                bus.BusTypeId = busType.BusTypeId;
            }
            db.Entry(bus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
