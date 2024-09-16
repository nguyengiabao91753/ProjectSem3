namespace ProjectSem3.DTOs;

public class PolicyDTO
{
    public int PolicyId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; } = null!;

    public byte Status { get; set; }
}
