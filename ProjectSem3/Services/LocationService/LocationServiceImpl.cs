using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.LocationService;

public class LocationServiceImpl : LocationService
{
    private DatabaseContext db;
    private IMapper mapper;
    public LocationServiceImpl(
        DatabaseContext databaseContext,
        IMapper mapper
        )
    {
        this.db = databaseContext;
        this.mapper = mapper;
    }

    public bool CheckLocationNameExists(string name)
    {
        return db.Locations.Any(b => b.Name == name);
    }

    public bool Create(LocationDTO locationDTO)
    {
        try
        {
            var location = mapper.Map<Location>(locationDTO);
            db.Locations.Add(location);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            // Ghi log lỗi nếu cần thiết
            Console.WriteLine(ex.Message); // In ra console để kiểm tra lỗi
            return false;
        }
    }

    public bool Delete(int id)
    {
        var location = db.Locations.SingleOrDefault(a => a.LocationId == id);
        if (location != null)
        {
            db.Locations.Remove(location);
            return db.SaveChanges() > 0; // Trả về true nếu xóa thành công
        }
        return false; // Trả về false nếu không tìm thấy location
    }

    public List<LocationDTO> GetAll()
    {
        return mapper.Map<List<LocationDTO>>(db.Locations.ToList());
    }

    public bool Update(LocationDTO locationDTO)
    {
        try
        {
            var location = mapper.Map<Location>(locationDTO);
            db.Entry(location).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        (Exception ex)
        {
            return false;
        }
    }
}
