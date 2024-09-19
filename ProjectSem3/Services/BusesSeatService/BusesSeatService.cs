using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BusesSeatService;

public interface BusesSeatService
{
    public List<BusesSeatDTO> GetAll();
    public List<BusesSeatDTO> GetSeatsByBusId(int busId);
    public bool Create(BusesSeat busesSeat);
    public bool Update(BusesSeatDTO busesSeatDTO);
    public bool Disable(int id);
}
