using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.AccountService;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ProjectSem3.Models;
using PayPal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Apis.Auth;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using Newtonsoft.Json.Linq;

namespace ProjectSem3.Controllers;
[Route("api/accountUser")]

public class AccountUserController : Controller
{
    private AccountUserService accountUserService;
    private IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;
    public AccountUserController(AccountUserService _accountUserService, IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
    {
        accountUserService = _accountUserService;
        configuration = _configuration;
        webHostEnvironment = _webHostEnvironment;
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("createAccountUser")]
    public IActionResult CreateAccountUser([FromBody] AccountUserDTO accountUserDTO)
    {
        try
        {
            if (accountUserDTO == null)
            {
                return BadRequest(new { error = "invalid account data" });
            }

            var existingAccount = accountUserService.FindByUsername(accountUserDTO.Username);   

            if (existingAccount != null)
            {
                return BadRequest(new { error = "Username already exists" });
            }


            accountUserDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountUserDTO.Password);

            bool result = accountUserService.CreateAccountUser(accountUserDTO);

            if (result)
            {
                return Ok(new { status = "Account created successfully" });

            }
            else
            {
                return BadRequest(new { error = "Error during account creation" });

            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error during account creation: " + ex.Message);
            return StatusCode(500, new
            {
                error = "Error during registration",
                details = ex.Message
            });
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("createAccountUserGg")]
    public IActionResult CreateAccountUserGg([FromBody] AccountUserDTO accountUserDTO)
    {
        try
        {
            if (accountUserDTO == null)
            {
                return BadRequest(new { error = "invalid account data" });
            }

            var existingAccount = accountUserService.FindByUsername(accountUserDTO.Username);
            if (existingAccount != null)
            {
                return Ok(existingAccount); // Nếu tài khoản đã tồn tại, trả về thông tin của nó
            }
    
            accountUserDTO.LevelId = 3; // Đặt level mặc định (tùy vào yêu cầu của bạn)
            bool result = accountUserService.CreateAccountUserGg(accountUserDTO);

            if (result)
            {
                return Ok(new { status = "Account created successfully" });

            }
            else
            {
                return BadRequest(new { error = "Error during account creation" });

            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error during account creation: " + ex.Message);
            return StatusCode(500, new
            {
                error = "Error during registration",
                details = ex.Message
            });
        }
    }


 
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("updateAccountUser")]
    public IActionResult UpdateAccountUser([FromBody] AccountUserDTO accountUserDTO)
    {
        try
        {
            // Gọi đến service để cập nhật dữ liệu
            bool result = accountUserService.UpdateAccountUser(accountUserDTO);

            if (result)
            {
                return Ok(new { status = "Account updated successfully" });
            }
            else
            {
                return BadRequest(new { error = "Error during account update" });
            }
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ và trả về lỗi
            return StatusCode(500, new { error = "Error during update", details = ex.Message });
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("updatePassword")]
    public IActionResult UpdatePassword([FromBody] AccountUserDTO accountUserDTO)
    {
        try
        {
            // Gọi đến service để cập nhật dữ liệu
            bool result = accountUserService.UpdatePassword(accountUserDTO);

            if (result)
            {
                return Ok(new { status = "Password updated successfully" });
            }
            else
            {
                return BadRequest(new { error = "Error during account update" });
            }
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ và trả về lỗi
            return StatusCode(500, new { error = "Error during update", details = ex.Message });
        }
    }

    [HttpPut("updateAccountUserToken")]
    [Authorize]
    public IActionResult UpdateAccountUserToken([FromBody] AccountUserDTO accountUserDTO)
    {
        // Lấy userId từ token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized(new { error = "User not found" });
        }

        int userId = int.Parse(userIdClaim);

        // Kiểm tra xem userId trong token có khớp với userId được gửi lên không
        if (userId != accountUserDTO.UserId)
        {
            return Unauthorized(new { error = "User ID mismatch" });
        }
        // Tạo một đối tượng DTO chỉ chứa các thông tin được phép cập nhật
        var updateDTO = new AccountUserDTO
        {
            UserId = userId,
            FullName = accountUserDTO.FullName,
            Email = accountUserDTO.Email,
            PhoneNumber = accountUserDTO.PhoneNumber,
            BirthDate = accountUserDTO.BirthDate,
            Address = accountUserDTO.Address,
            Password = accountUserDTO.Password // Nếu cho phép thay đổi mật khẩu
        };
        // Thực hiện cập nhật thông tin người dùng
        var result = accountUserService.UpdateUserProfile(updateDTO);
        if (result)
        {
            return Ok(new { status = true, message = "Profile updated successfully" });
        }
        else
        {
            return BadRequest(new { status = false, message = "Failed to update profile" });
        }
    }
    

    //[HttpGet("getInfoByToken")]
    //[Authorize]
    //public IActionResult GetUserProfile()
    //{
    //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //    if (string.IsNullOrEmpty(userIdClaim))
    //    {
    //        return Unauthorized(new { error = "User not found" });
    //    }

    //    int userId = int.Parse(userIdClaim);
    //    var userInfo = accountUserService.GetInfoAccountById(userId);

    //    if (userInfo == null)
    //    {
    //        return NotFound(new { error = "Account not found" });
    //    }

    //    return Ok(userInfo);
    //}

    [HttpGet("getInfoByToken")]
    [Authorize]
    public IActionResult GetUserProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized(new { error = "User not found" });
        }

        if (!int.TryParse(userIdClaim, out int userId))
        {
            return BadRequest(new { error = "Invalid User ID format" });
        }

        var userInfo = accountUserService.GetInfoAccountById(userId);

        if (userInfo == null)
        {
            return NotFound(new { error = "Account not found" });
        }

        return Ok(userInfo);
    }

    [Produces("application/json")]
    [HttpGet("getInfoByEmail/{email}")]
    public IActionResult GetInfoByEmail(string email)
    {
        try
        {
            var userInfo = accountUserService.FindByEmail(email);

            if (userInfo == null)
            {
                return NotFound(new { error = "Account not found" });
            }

            return Ok(userInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error fetching user information", details = ex.Message });
        }
    }


    [Produces("application/json")]
    [HttpGet("getInfoAccountById/{id}")]
    public IActionResult GetInfoAccountById(int id)
    {
        try
        {
            var userInfo = accountUserService.GetInfoAccountById(id);

            return Ok(userInfo);

        }
        catch (Exception ex)
        {
            // Xử lý lỗi và trả về BadRequest
            return BadRequest(ex.Message);
        }
    }
    
    [Produces("application/json")]
    [HttpGet("getAllAccountUserInfo")]
    public IActionResult GetAllAccountUserInfo()
    {
        try
        {
            return Ok(accountUserService.GetAllAccountUserInfo());
        }
        catch
        {

            return BadRequest();

        }
    }
    
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpDelete("deleteAccountUser/{id}")]
    public IActionResult DeleteAccountUser(int id)
    {
        try
        {
            // Gọi phương thức xóa
            var result = accountUserService.DeleteAccountUser(id);

            // Kiểm tra kết quả
            if (result)
            {
                return Ok(new
                {
                    Status = "Success",
                    Message = $"AccountUser with ID {id} deleted successfully."
                });
            }
            else
            {
                return NotFound(new
                {
                    Status = "Error",
                    Message = $"AccountUser with ID {id} not found or could not be deleted."
                });
            }
        }
        catch (Exception ex)
        {
            // Trả về thông tin chi tiết lỗi trong trường hợp có lỗi
            return StatusCode(500, new
            {
                Status = "Error",
                Message = $"An error occurred: {ex.Message}"
            });
        }
    }

    [HttpGet("checkUsername/{username}")]
    public IActionResult CheckUsername(string username)
    {
        // Kiểm tra xem username đã tồn tại chưa
        var existingAccount = accountUserService.FindByUsername(username);

        if (existingAccount != null)
        {
            // Username đã tồn tại
            return Ok(new { exists = "Username already exists" });
        }

        // Username chưa tồn tại
        return Ok(new { exists = false });
    }

    [HttpGet("checkEmail/{email}")]
    public IActionResult CheckEmail(string email)
    {
        // Kiểm tra xem email đã tồn tại chưa
        var existingAccount = accountUserService.FindByEmail(email);

        if (existingAccount != null)
        {
            // email đã tồn tại
            return Ok(new { exists = "Email already exists" });
        }

        // Username chưa tồn tại
        return Ok(new { exists = false });
    }


    [HttpPost("inactive")]
    public IActionResult Inactive([FromBody] AccountUserDTO accountUserDTO)
    {
        bool result = accountUserService.InActiveAccount(accountUserDTO);
        return result ? Ok(new { status = "Account set to inactive successfully" })
                      : BadRequest(new { error = "Failed to set account to inactive" });
    }

    [HttpPost("active")]
    public IActionResult Active([FromBody] AccountUserDTO accountUserDTO)
    {
        bool result = accountUserService.ActiveAccount(accountUserDTO);
        return result ? Ok(new { status = "Account set to active successfully" })
                      : BadRequest(new { error = "Failed to set account to active" });
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] AccountUserDTO accountUserDTO)
    {
        var account = accountUserService.Login(accountUserDTO.Username, accountUserDTO.Password);
        if (account != null)
        {
            var token = accountUserService.GenerateJSONWebToken(account);
            return Ok(new
            {
                token = token,
                userId = account.UserId,
                email = account.Email,
                levelId = account.LevelId,
                status = account.Status
            });
        }
        return Unauthorized(new { message = "Invalid credentials" });
    }


    //[HttpPost("loginWithGoogle")]
    //public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginDTO googleLoginDTO)
    //{
    //    var payload = await accountUserService.VerifyGoogleToken(googleLoginDTO.IdToken);
    //    if (payload == null)
    //    {
    //        return Unauthorized(new { message = "Invalid Google token" });
    //    }

    //    // Tạo hoặc lấy thông tin người dùng từ database
    //    var accountUserDTO = await accountUserService.GetOrCreateUserFromGoogleAsync(payload);

    //    // Phát hành JWT token của server
    //    var token = accountUserService.GenerateJSONWebToken(accountUserDTO);
    //    return Ok(new
    //    {
    //        token = token,
    //        userId = accountUserDTO.UserId,
    //        email = accountUserDTO.Email,
    //        levelId = accountUserDTO.LevelId,
    //        status = accountUserDTO.Status
    //    });
    //}
    //        return Unauthorized(new { message = "Invalid Google Token" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, new { message = "An error occurred while validating Google token", details = ex.Message });
    //    }
    //}


    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] string credential)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { configuration.GetValue<string>("Google:ClientID") }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

            var account = accountUserService.FindByEmail(payload.Email);


            if (account == null)
            {
                // Tạo tài khoản mới
                var user = new AccountUserDTO
                {
                    Username = payload.Email,
                    Password = null,
                    Status = 1,
                    LevelId = 3,
                    FullName = payload.Name,
                    BirthDate = DateTime.Now.ToString(),
                    Email = payload.Email,
                    PhoneNumber = null,
                    Address = null,
                    CreatedAt = DateTime.Now.ToString()
                };



                var response = accountUserService.CreateAccountUser(user);
                if(response)
                {
                    account = accountUserService.FindByEmail(user.Email);

                }
                else
                {
                    return BadRequest(new { message = "Login Google Fail"});

                }
            }
            var token = accountUserService.GenerateJSONWebToken(account);
            return Ok(new
            {
                token = token,
                userId = account.UserId,
                email = account.Email,
                levelId = account.LevelId,
                status = account.Status
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Login Google Fail", details = ex.Message });
        }
       
    }

}
