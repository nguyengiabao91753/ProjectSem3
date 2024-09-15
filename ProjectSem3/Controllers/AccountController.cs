using Microsoft.AspNetCore.Mvc;
using ProjectSem3.DTOs;
using ProjectSem3.Services.AccountService;
using ProjectSem3.Services.UserService;

namespace ProjectSem3.Controllers;
[Route("api/account")]

public class AccountController :Controller
{
    private AccountUserService accountService;
    private UserService userService;
    public AccountController(AccountUserService _accountService, UserService _userService)
    {
        accountService = _accountService;
        userService = _userService;
    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin user)
    {
        try
        {
            if (userService.Login(user))
            {

                    return Ok(new
                    {
                        Token = accountService.GenerateJSONWebToken(user.Email)
                    });
            }
            return Ok();

        }
        catch
        {
                
            return BadRequest(new { Error = "Error" });

        }
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDTO userDTO,AccountDTO accountDTO)
    {
        try
        {
            if(userDTO == null)
            {
                return BadRequest(new { error = "invalid user data" });

            }
            if (userService.CreateUser(userDTO))
            {
                return Ok(new { message = "usser registered successfully" });

            }
            if (accountDTO == null)
            {
                return BadRequest(new { error = "invalid account data" });
            }
            accountDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);
            if (accountService.CreateAccount(accountDTO))
            {
                return Ok(new { message = "account registered successfully" });
            }
            return BadRequest(new { error = "registration failed" });
        }
        catch
        {
            return BadRequest(new { error = "error during registration" });
        }

    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("update")]
    public IActionResult Update([FromBody] UserDTO userDTO, AccountDTO accountDTO)
    {
        try
        {
            if (userDTO == null)
            {
                return BadRequest(new { error = "invalid user data" });

            }
            if (userService.UpdateUser(userDTO))
            {
                return Ok(new { message = "usser registered successfully" });

            }
            if (accountDTO == null)
            {
                return BadRequest(new { error = "invalid account data" });

            }
            if (accountService.UpdateAccount(accountDTO))
            {
                
                //accountDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);
                return Ok(new
                {
                    Result = accountService.UpdateAccount(accountDTO)

                });
            }
            return BadRequest(new { error = "registration failed" });

        }
        catch
        {

            return BadRequest(new { error = "error during eidt" });

        }
    }

    [Produces("application/json")]
    [HttpGet("getUserInfoByEmail/{email}")]
    public IActionResult GetUserInfoByEmail(string email)
    {
        try
        {
            return Ok(userService.getUserInfoByEmail(email));
        }
        catch
        {

            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("getInfoByAccountId/{id}")]
    public IActionResult GetInfoByAccountId(int id)
    {
        try
        {
            var userInfo = userService.getUserInfoById(id);

            var accountInfo = accountService.getInfoByAccountId(id);

            if (userInfo != null && accountInfo != null)
            {
                var combinedInfo = new
                {
                    User = userInfo,
                    Account = accountInfo
                };

                return Ok(combinedInfo);
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            // Xử lý lỗi và trả về BadRequest
            return BadRequest(ex.Message);
        }
    }
    //hỏi nghĩa xem đoạn code trên cos ổn không?
}
