using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BusesSeatService;

public class BusesSeatServiceImpl : BusesSeatService
{
    private readonly DatabaseContext db;
    private readonly IMapper mapper;

    public BusesSeatServiceImpl(DatabaseContext _db, IMapper _mapper)
    {
        db = _db;
        mapper = _mapper;
    }

    public bool Create(BusesSeatDTO busesSeatDTO)
    {
        try
        {
            var busesSeat = mapper.Map<BusesSeat>(busesSeatDTO);
            var bus = db.Buses.SingleOrDefault(b => b.LicensePlate == busesSeatDTO.BusLicensePlate);
            if (bus != null)
            {
                if (busesSeatDTO.BusId != null && busesSeatDTO.BusId != bus.BusId)
                {
                    throw new InvalidOperationException("BusId does not match the BusLicensePlate provided.");
                }

                busesSeat.BusId = bus.BusId;
            }
            db.BusesSeats.Add(busesSeat);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Disable(int id)
    {
        var busesSeat = db.BusesSeats.SingleOrDefault(b => b.SeatId == id);
        if (busesSeat != null)
        {
            busesSeat.Status = 0;
            return db.SaveChanges() > 0;
        }
        var busesSeatDTO = mapper.Map<BusesSeatDTO>(busesSeat);
        return Update(busesSeatDTO);
    }

    public List<BusesSeatDTO> GetAll()
    {
        return mapper.Map<List<BusesSeatDTO>>(db.BusesSeats.ToList());
    }

    public List<BusesSeatDTO> GetSeatsByBusId(int busId)
    {
        return mapper.Map<List<BusesSeatDTO>>(db.BusesSeats.Where(b => b.BusId == busId).ToList());
    }

    public bool Update(BusesSeatDTO busesSeatDTO)
    {
        try
        {
            var busesSeat = mapper.Map<BusesSeat>(busesSeatDTO);
            var bus = db.Buses.SingleOrDefault(b => b.LicensePlate == busesSeatDTO.BusLicensePlate);
            if (bus != null)
            {
                if (busesSeatDTO.BusId != null && busesSeatDTO.BusId != bus.BusId)
                {
                    throw new InvalidOperationException("BusId does not match the BusLicensePlate provided.");
                }

                busesSeat.BusId = bus.BusId;
            }
            db.Entry(busesSeat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
