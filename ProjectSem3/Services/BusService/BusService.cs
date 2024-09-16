using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BusService;

public interface BusService
{
    public List<BusDTO> GetAll();
    public BusDTO findById(int id);
    public bool Create(BusDTO busDTO);
    public bool Update(BusDTO busDTO);
    public bool Disable(int id);
}
