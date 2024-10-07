using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.LevelService;

public class LevelServiceImpl : LevelService
{
    private DatabaseContext db;
    private IMapper mapper;
    public LevelServiceImpl(DatabaseContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }



    public LevelDTO getLevelById(int id)
    {
        return mapper.Map<LevelDTO>(db.Levels.Find(id));
    }

    public List<LevelDTO> GetAllLevel()
    {
        return mapper.Map<List<LevelDTO>>(db.Levels.ToList());
    }

    
}
