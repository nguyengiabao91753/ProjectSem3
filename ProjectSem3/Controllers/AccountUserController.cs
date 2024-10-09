using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.AccountService;
using System.Security.Claims;
using Google.Apis.Auth;

namespace ProjectSem3.Controllers;
[Route("api/accountUser")]

public class AccountUserController :Controller
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

    [HttpPost("login")]
    public IActionResult Login([FromBody] AccountUserDTO accountUserDTO)
    {
        var account = accountUserService.Login(accountUserDTO.Username, accountUserDTO.Password);
        if (account != null)
        {
            var token = accountUserService.GenerateJSONWebToken(accountUserDTO.Username, account.UserId);
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
                    return Ok(new {status = "Account created successfully" });

                }
                else
                {
                    return BadRequest(new { error = "Error during account creation" });

                }
            }
          
         catch (Exception ex)
        {
            Console.WriteLine("Error during account creation: " + ex.Message);
            return StatusCode(500, new { error = "Error during registration", details = ex.Message
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
            return Ok(new { status = "Username already exists" });
        }

        // Username chưa tồn tại
        return Ok(new { exists = false });
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
    [HttpPost("loginWithGoogle")]
    public async Task<IActionResult> LoginWithGoogle([FromBody] string idToken)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { configuration["Google:ClientId"] }
            });

            if (payload != null)
            {
                var user = accountUserService.FindByUsername(payload.Email);
                if (user != null)
                {
                    var token = accountUserService.GenerateJSONWebToken(user.Username, user.UserId);
                    return Ok(new { token, userId = user.UserId });
                }
            }

            return Unauthorized(new { message = "Invalid Google Token" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while validating Google token", details = ex.Message });
        }
    }




}
