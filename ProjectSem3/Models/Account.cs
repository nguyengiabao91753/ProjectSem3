using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte? Status { get; set; }

    public int? LevelId { get; set; }

    public virtual User AccountNavigation { get; set; } = null!;

    public virtual Level? Level { get; set; }
}
