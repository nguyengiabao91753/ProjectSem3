﻿using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.AgeGroupService;

public class AgeGroupServiceImpl : AgeGroupService
{
    private DatabaseContext db;
    private IMapper mapper;
    public AgeGroupServiceImpl(
        DatabaseContext databaseContext,
        IMapper mapper
        )
    {
        this.db = databaseContext;
        this.mapper = mapper;
    }

    public bool Create(AgeGroupDTO ageGroupDTO)
    {
        try
        {
            var ageGroup = mapper.Map<AgeGroup>(ageGroupDTO);
            db.AgeGroups.Add(ageGroup);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        var age = db.AgeGroups.SingleOrDefault(a => a.AgeGroupId == id);
        age.Status = 0;
        var agedto = mapper.Map<AgeGroupDTO>(age);
        return Update(agedto);
    }

    public List<AgeGroupDTO> GetAll()
    {
        return mapper.Map<List<AgeGroupDTO>>(db.AgeGroups.ToList());
    }

    public bool Update(AgeGroupDTO ageGroupDTO)
    {
        try
        {
            var ageGroup = mapper.Map<AgeGroup>(ageGroupDTO);
            db.Entry(ageGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        (Exception ex)
        {
            return false;
        }
    }
}
