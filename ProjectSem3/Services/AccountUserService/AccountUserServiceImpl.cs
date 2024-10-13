using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectSem3.DTOs;
using ProjectSem3.Models;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectSem3.Services.AccountService;

public class AccountUserServiceImpl(DatabaseContext db, IMapper mapper, IConfiguration configuration) : AccountUserService
{

    private readonly DatabaseContext databaseContext = db;
    private readonly IMapper mapper = mapper;
    private readonly IConfiguration configuration = configuration;

    public bool CreateAccountUser(AccountUserDTO accountUserDTO)
    {
        using var transaction = db.Database.BeginTransaction(); // Dùng transaction để đảm bảo dữ liệu an toàn

        try
        {
            var existingAccount = db.Accounts.FirstOrDefault(a => a.Username == accountUserDTO.Username);
            if (existingAccount != null)
            {
                return false;
            }

            var user = mapper.Map<User>(accountUserDTO);
            user.CreatedAt = DateTime.Now;

            db.Users.Add(user);
            db.SaveChanges();

            var account = mapper.Map<Account>(accountUserDTO);
            account.AccountId = user.UserId;
            db.Accounts.Add(account);
            db.SaveChanges();

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Error during account creation: {ex.Message}");
            return false;
        }

    }

    public string GenerateJSONWebToken(string username, int userId)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    //new Claim("userId", userId.ToString()) // Use custom claim name 'userId'

        }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }



    public AccountUserDTO GetInfoAccountById(int id)
    {
        try
        {
            var account = db.Accounts
                .Include(a => a.AccountNavigation)
                .FirstOrDefault(a => a.AccountId == id);

            if (account == null)
            {
                return null;
            }

            if (account.AccountNavigation == null)
            {
                Console.WriteLine($"No User associated with AccountId {id}");
                return null;
            }

            return new AccountUserDTO
            {
                UserId = account.AccountNavigation.UserId,
                Username = account.Username,
                FullName = account.AccountNavigation.FullName,
                Email = account.AccountNavigation.Email,
                PhoneNumber = account.AccountNavigation.PhoneNumber,
                BirthDate = account.AccountNavigation.BirthDate.ToString("dd-MM-yyyy"),
                Address = account.AccountNavigation.Address,
                Status = account.Status,
                LevelId = account.LevelId,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetInfoAccountById: {ex.Message}");
            return null;
        }
    }


    public bool UpdateAccountUser(AccountUserDTO accountUserDTO)
    {
        try
        {
            // Tìm Account và User bằng UserId
            var currentAccount = db.Accounts.FirstOrDefault(a => a.AccountId == accountUserDTO.UserId);
            var user = db.Users.FirstOrDefault(u => u.UserId == accountUserDTO.UserId);

            // Kiểm tra xem cả Account và User có tồn tại không
            if (currentAccount == null || user == null)
            {
                return false;
            }

            // Cập nhật thông tin tài khoản (Account)
            currentAccount.Username = accountUserDTO.Username;
            currentAccount.Status = accountUserDTO.Status;
            currentAccount.LevelId = accountUserDTO.LevelId;

            if (!string.IsNullOrEmpty(accountUserDTO.Password))
            {
                currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(accountUserDTO.Password);
            }

            // Cập nhật thông tin người dùng (User)
            user.FullName = accountUserDTO.FullName;
            user.BirthDate = DateTime.ParseExact(accountUserDTO.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            user.Email = accountUserDTO.Email;
            user.PhoneNumber = accountUserDTO.PhoneNumber;
            user.Address = accountUserDTO.Address;

            db.Entry(currentAccount).State = EntityState.Modified;
            db.Entry(user).State = EntityState.Modified;

            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during update: {ex.Message}");
            return false;
        }
    }

    public bool UpdateUserProfile(AccountUserDTO accountUserDTO)
    {
        try
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == accountUserDTO.UserId);
            var account = db.Accounts.FirstOrDefault(a => a.AccountId == accountUserDTO.UserId);

            if (user == null || account == null)
            {
                return false;
            }

            // Không cho phép cập nhật Username, Status, LevelId

            // Cập nhật thông tin người dùng (User)
            user.FullName = accountUserDTO.FullName;
            user.BirthDate = DateTime.ParseExact(accountUserDTO.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            user.Email = accountUserDTO.Email;
            user.PhoneNumber = accountUserDTO.PhoneNumber;
            user.Address = accountUserDTO.Address;

            // Cập nhật mật khẩu nếu có
            if (!string.IsNullOrEmpty(accountUserDTO.Password))
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(accountUserDTO.Password);
                db.Entry(account).State = EntityState.Modified;
            }

            db.Entry(user).State = EntityState.Modified;

            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during profile update: {ex.Message}");
            return false;
        }
    }


    public List<AccountUserDTO> GetAllAccountUserInfo()
    {
        var accountUserInfo = (from account in db.Accounts
                               join user in db.Users on account.AccountId equals user.UserId
                               select new AccountUserDTO
                               {
                                   UserId = user.UserId,
                                   FullName = user.FullName,
                                   BirthDate = user.BirthDate.ToString("dd-MM-yyyy"), // Chuyển DateTime thành chuỗi
                                   Email = user.Email,
                                   PhoneNumber = user.PhoneNumber,
                                   Address = user.Address,
                                   CreatedAt = user.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), // Chuyển DateTime thành chuỗi
                                   Username = account.Username,
                                   Password = account.Password,
                                   Status = account.Status,
                                   LevelId = account.LevelId
                               }).ToList();

        return accountUserInfo;
    }



    public AccountUserDTO Login(string username, string password)
    {
        var account = db.Accounts.FirstOrDefault(a => a.Username == username);
        if (account == null)
        {
            return null;
        }
        //if (account == null || string.IsNullOrEmpty(account.Password) ||
        //    !BCrypt.Net.BCrypt.Verify(password, account.Password))
        //{
        //    return null;
        //}
        if (!BCrypt.Net.BCrypt.Verify(password, account.Password))
        {
            return null;
        }
        return mapper.Map<AccountUserDTO>(account);
    }



    public bool DeleteAccountUser(int id)
    {
        try
        {
            // Tìm Account và User theo id
            var account = db.Accounts.Find(id);
            var user = db.Users.Find(id);

            // Kiểm tra nếu tìm thấy cả account và user
            if (account != null && user != null)
            {
                db.Accounts.Remove(account);
                db.Users.Remove(user);

                // Lưu thay đổi
                return db.SaveChanges() > 0;
            }

            // Nếu không tìm thấy account hoặc user
            return false;
        }
        catch (Exception ex)
        {
            // Ghi lại lỗi nếu cần
            return false;
        }
    }

    public AccountUserDTO FindByUsername(string username)
    {
        var account = db.Accounts.Include(a => a.AccountNavigation)
                                 .FirstOrDefault(a => a.Username == username);
        if (account == null) return null;

        return mapper.Map<AccountUserDTO>(account);
    }

    public bool InActiveAccount(AccountUserDTO accountUserDTO)
    {
        try
        {
            // Tìm Account và User theo id
            var account = db.Accounts.Find(accountUserDTO.UserId);
            var user = db.Users.Find(accountUserDTO.UserId);
            // Kiểm tra nếu tìm thấy cả account và user
            if (account != null && account.Status != 0)
            {
                account.Status = 0;
                // Lưu thay đổi
                return db.SaveChanges() > 0;
            }
            return false;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in InActiveAccount: {ex.Message}");
            return false;
        }
    }

    public bool ActiveAccount(AccountUserDTO accountUserDTO)
    {
        try
        {
            // Tìm Account và User theo id
            var account = db.Accounts.Find(accountUserDTO.UserId);
            var user = db.Users.Find(accountUserDTO.UserId);
            // Kiểm tra nếu tìm thấy cả account và user
            if (account != null && account.Status == 0)
            {
                account.Status = 1;
                // Lưu thay đổi
                return db.SaveChanges() > 0;
            }
            return false;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in InActiveAccount: {ex.Message}");
            return false;
        }
    }
}
