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

public class AccountUserServiceImpl(DatabaseContext db, IMapper mapper,IConfiguration configuration) : AccountUserService
{

    private readonly DatabaseContext databaseContext = db;
    private readonly IMapper mapper = mapper;
    private readonly IConfiguration configuration = configuration;

    public bool CreateAccountUser(AccountUserDTO accountUserDTO)
    {
  
            try
            {
                //Kiểm tra xem username đã tồn tại hay chưa
                var existingAccount = db.Accounts.FirstOrDefault(a=> a.Username == accountUserDTO.Username);
                if (existingAccount != null)
                {   
                    //Username đã tòn tại
                    return false;
                }
                // Map accountUserDTO sang Account
                var account = mapper.Map<Account>(accountUserDTO);
                
                // Map accountUserDTO sang User, gán AccountId cho User
                var user = mapper.Map<User>(accountUserDTO);
                user.CreatedAt = DateTime.Now;
                db.Users.Add(user);
                
                if (db.SaveChanges()>0)
                {
                    account.AccountId = user.UserId;
                
                    db.Accounts.Add(account);
                
                    // Lưu thay đổi cho cả hai bảng
                    return db.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Rollback transaction nếu có lỗi
                return false;
            }
        
    }

    public string GenerateJSONWebToken(string username)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, username),

            }),
            Issuer = issuer,
            Audience = audience,
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public AccountUserDTO GetInfoByAccountById(int id)
    {
        return mapper.Map<AccountUserDTO>(db.Users.Find(id));
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

            // Hash lại mật khẩu nếu có thay đổi
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

            // Đánh dấu là đã thay đổi
            db.Entry(currentAccount).State = EntityState.Modified;
            db.Entry(user).State = EntityState.Modified;

            // Lưu thay đổi
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            // Ghi log lỗi nếu cần
            Console.WriteLine($"Error during update: {ex.Message}");
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
        // Tìm account dựa trên username
        var account = db.Accounts.FirstOrDefault(a => a.Username == username);
        if (account == null)
        {
            // Không tìm thấy tài khoản
            return null;
        }
        // Kiểm tra account và mật khẩu
        if (account == null || string.IsNullOrEmpty(account.Password) ||
            !BCrypt.Net.BCrypt.Verify(password, account.Password))
        {
            return null; // Trả về null hoặc một thông báo lỗi tùy theo yêu cầu của bạn
        }

        // Nếu tài khoản và mật khẩu hợp lệ, trả về đối tượng AccountDTO
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

    public Account FindByUsername(string username)
    {
        return db.Accounts.FirstOrDefault(a => a.Username == username);
    }
}
