using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BusTypeService;

public class BusTypeServiceImpl : BusTypeService
{
    private readonly DatabaseContext db;
    private readonly IMapper mapper;

    public BusTypeServiceImpl(DatabaseContext _db, IMapper _mapper)
    {
        db = _db;
        mapper = _mapper;
    }

    public bool Create(BusTypeDTO busTypeDTO)
    {
        try
        {
            var busType = mapper.Map<BusType>(busTypeDTO);
            db.BusTypes.Add(busType);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Disable(int id)
    {
        var busType = db.BusTypes.SingleOrDefault(b => b.BusTypeId == id);
        if (busType != null)
        {
            busType.Status = 0;
            return db.SaveChanges() > 0;
        }
        var busTypeDTO = mapper.Map<BusTypeDTO>(busType);
        return Update(busTypeDTO);
    }

    public BusTypeDTO FindByName(string name)
    {
        try
        {
            return mapper.Map<BusTypeDTO>(db.BusTypes.FirstOrDefault(b=>b.Name.ToUpper().Equals(name.ToUpper())));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<BusTypeDTO> GetAll()
    {
        return mapper.Map<List<BusTypeDTO>>(db.BusTypes.ToList());
    }

    public bool Update(BusTypeDTO busTypeDTO)
    {
        try
        {
            var busType = mapper.Map<BusType>(busTypeDTO);
            db.Entry(busType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
