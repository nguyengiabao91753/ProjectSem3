namespace ProjectSem3.DTOs;

public class LevelDTO
{
    public int LevelId { get; set; }

    public string? Name { get; set; }

    public byte? Status { get; set; }

}
public class AccountUserDTO
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public byte? Status { get; set; }

    public int? LevelId { get; set; }

    public string FullName { get; set; }

    public string BirthDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? CreatedAt { get; set; }

}
public class LoginDTO
{
    public string Username { get; set; }

    public string? Password { get; set; }

}
public class AccountDTO
{
    public int AccountId { get; set; }
    public string Username { get; set; }

    public string? Password { get; set; }

    public byte? Status { get; set; }

    public int? LevelId { get; set; }
}
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