using Google.Apis.Auth;
using ProjectSem3.DTOs;
using ProjectSem3.Models;

namespace ProjectSem3.Services.AccountService;

public interface AccountUserService
{
    public AccountUserDTO Login(string username, string password);
    public string GenerateJSONWebToken(AccountUserDTO accountUserDTO);
    public AccountUserDTO GetInfoAccountById(int id);
    public List<AccountUserDTO> GetAllAccountUserInfo();
    public AccountUserDTO FindByUsername(string username);
    public AccountUserDTO FindByEmail(string email);
    public bool CreateAccountUser(AccountUserDTO accountUserDTO);
    public bool CreateAccountUserGg(AccountUserDTO accountUserDTO);
    public bool UpdateAccountUser(AccountUserDTO accountUserDTO);
    public bool UpdateUserProfile(AccountUserDTO accountUserDTO);

    public bool UpdatePassword(AccountUserDTO accountUserDTO);

    public bool DeleteAccountUser(int id);
    public bool InActiveAccount(AccountUserDTO accountUserDTO);
    public bool ActiveAccount(AccountUserDTO accountUserDTO);


    public  Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken);

    public  Task<AccountUserDTO> GetOrCreateUserFromGoogleAsync(GoogleJsonWebSignature.Payload payload);

}
