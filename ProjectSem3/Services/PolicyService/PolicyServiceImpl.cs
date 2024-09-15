using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.PolicyService;

public class PolicyServiceImpl : PolicyService
{
    private DatabaseContext db;
    private IMapper mapper;
    public PolicyServiceImpl(
        DatabaseContext databaseContext,
        IMapper mapper
        )
    {

        this.db = databaseContext;
        this.mapper = mapper;
    }

    public bool create(PolicyDTO policyDTO)
    {
        try
        {
            var poli = mapper.Map<Policy>( policyDTO );
            db.Policies.Add( poli );
            return db.SaveChanges() > 0;
        }catch ( Exception ex )
        {
            return false;
        }
    }

    public List<PolicyDTO> getall()
    {
        return mapper.Map<List<PolicyDTO>>( db.Policies.ToList());
    }

    public bool update(PolicyDTO policyDTO)
    {
        try
        {
            var poli = mapper.Map<Policy>(policyDTO);
            db.Entry(poli).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch ( Exception ex )
        {
            return false;
        }
    }
}
