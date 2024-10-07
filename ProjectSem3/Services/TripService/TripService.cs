using ProjectSem3.DTOs;

namespace ProjectSem3.Services.TripService;

public interface TripService
{
    public bool Create(TripDTO tripDTO);

    public List<TripDTO> GetAll();

    public bool Update(TripDTO tripDTO);

    public bool Delete(int id);
    public TripDTO findById(int id);
    public interface ITripService
    {
        Task<bool> IsTripExistAsync(int departureLocationId, int arrivalLocationId, DateTime dateStart);
    }

}
