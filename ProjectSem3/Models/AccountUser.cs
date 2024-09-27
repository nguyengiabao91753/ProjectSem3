namespace ProjectSem3.Models;

public class AccountUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte? Status { get; set; }

    public int? LevelId { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }

}

