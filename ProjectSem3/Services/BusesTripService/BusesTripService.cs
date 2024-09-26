using ProjectSem3.DTOs;

namespace ProjectSem3.Services.BusesTripService;

public interface BusesTripService
{
    public bool Create(BusesTripDTO busesTripDTO);

    public bool Update(BusesTripDTO busesTripDTO);

    public bool Done(BusesTripDTO busesTripDTO);

    public List<BusesTripDTO> GetAll();

    public BusesSeatDTO GetById(int id);
}
