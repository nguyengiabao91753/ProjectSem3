using ProjectSem3.DTOs;

namespace ProjectSem3.Services.LevelService;

public interface LevelService
{
    public LevelDTO getLevelById(int id);

    public List<LevelDTO> GetAllLevel();


}
