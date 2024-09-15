using ProjectSem3.DTOs;

namespace ProjectSem3.Services.AccountService;

public interface AccountUserService
{
    public bool Login(string email, string password);
    public string GenerateJSONWebToken(string email);
    public AccountDTO getInfoByAccountId(int id);
    public List<AccountDTO> GetAllAccountInfo();

    public bool CreateAccount(AccountDTO accountDTO);
    public bool UpdateAccount(AccountDTO accountDTO);
}
