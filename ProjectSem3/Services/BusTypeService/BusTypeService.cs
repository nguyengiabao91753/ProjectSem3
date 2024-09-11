using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BusTypeService;

public interface BusTypeService
{
    public List<BusTypeDTO> GetAll();

    public bool Create(BusTypeDTO busTypeDTO);
    public bool Update(BusTypeDTO busTypeDTO);
    public bool Disable(int id);
}
