using ProjectSem3.DTOs;

namespace ProjectSem3.Services.AgeGroupService;

public interface AgeGroupService
{
    public bool Create(AgeGroupDTO ageGroupDTO);

    public List<AgeGroupDTO> GetAll();

    public bool Update(AgeGroupDTO ageGroupDTO);

    public bool Delete(int id);
    
}
