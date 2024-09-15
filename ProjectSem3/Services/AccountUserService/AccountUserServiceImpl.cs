using AutoMapper;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.AccountService;

public class AccountUserServiceImpl(DatabaseContext db, IMapper mapper) : AccountUserService
{

    private readonly DatabaseContext databaseContext = db;
    private readonly IMapper mapper = mapper;

    public bool CreateAccount(AccountDTO accountDTO)
    {
        throw new NotImplementedException();
    }

    public string GenerateJSONWebToken(string email)
    {
        throw new NotImplementedException();
    }

    public AccountDTO getInfoByAccountId(int id)
    {
        return mapper.Map<AccountDTO>(db.Accounts.Find(id));
    }

    public bool UpdateAccount(AccountDTO accountDTO)
    {
        throw new NotImplementedException();
    }

    public List<AccountDTO> GetAllAccountInfo()
    {
        throw new NotImplementedException();
    }


    public bool Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}
