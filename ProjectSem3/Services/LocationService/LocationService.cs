using ProjectSem3.DTOs;

namespace ProjectSem3.Services.LocationService;

public interface LocationService
{
    public bool Create(LocationDTO locationDTO);

    public List<LocationDTO> GetAll();

    public bool Update(LocationDTO locationDTO);

    public bool Delete(int id);

}
