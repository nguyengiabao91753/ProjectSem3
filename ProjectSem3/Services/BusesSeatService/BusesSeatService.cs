using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BusesSeatService;

public interface BusesSeatService
{
    public List<BusesSeatDTO> GetAll();
    public List<BusesSeatDTO> GetSeatsByBusId(int busId);
    public bool Create(BusesSeatDTO busesSeatDTO);
    public bool Update(BusesSeatDTO busesSeatDTO);
    public bool Disable(int id);
}
