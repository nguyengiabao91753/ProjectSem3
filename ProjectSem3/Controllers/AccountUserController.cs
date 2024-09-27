using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.AccountService;
using ProjectSem3.Services.AgeGroupService;

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
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("login")]
    public IActionResult Login([FromBody] AccountUserDTO accountUserDTO)
    {
        var account = accountUserService.Login(accountUserDTO.Username, accountUserDTO.Password);
        if (account != null)
        {
            var token = accountUserService.GenerateJSONWebToken(accountUserDTO.Username);
            return Ok(new { token = token }); // Trả về token trong JSON
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
            //kiểm tra xem username đã tồn tài hay chưa
            var existingAccount = accountUserService.FindByUsername(accountUserDTO.Username);   
            if (existingAccount != null)
            {
                Console.WriteLine("Username already exists");
                return BadRequest(new { error = "Username already exists" });
            }
            accountUserDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountUserDTO.Password);
            // gọi service để đăng ký tài khoản mới
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
            // Bắt lỗi ngoại lệ và trả về thông báo lỗi
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


    //[Produces("application/json")]
    //[HttpGet("getUserInfoByEmail/{email}")]
    //public IActionResult GetUserInfoByEmail(string email)
    //{
    //    try
    //    {
    //        return Ok(accountUserService.get(email));
    //    }
    //    catch
    //    {

    //        return BadRequest();
    //    }
    //}

    [Produces("application/json")]
    [HttpGet("getInfoByAccountId/{id}")]
    public IActionResult GetInfoByAccountId(int id)
    {
        try
        {
            var userInfo = accountUserService.GetInfoByAccountById(id);

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

}
