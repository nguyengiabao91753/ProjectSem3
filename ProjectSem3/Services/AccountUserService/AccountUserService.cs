using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.AccountService;

public interface AccountUserService
{
    public AccountUserDTO Login(string username, string password);
    public string GenerateJSONWebToken(string email);
    public AccountUserDTO GetInfoByAccountById(int id);
    public List<AccountUserDTO> GetAllAccountUserInfo();
    public Account FindByUsername(string username);
    public bool CreateAccountUser(AccountUserDTO accountUserDTO);
    public bool UpdateAccountUser(AccountUserDTO accountUserDTO);
    public bool DeleteAccountUser(int id);
}
