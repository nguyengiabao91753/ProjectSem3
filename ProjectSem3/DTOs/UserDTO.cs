using ProjectSem3.Models;

namespace ProjectSem3.DTOs;

public class UserDTO
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? BirthDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? CreatedAt { get; set; }




}


public class UserLogin
{
    public string Email { get; set; }
    public string Password { get; set; }

}