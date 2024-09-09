using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Level
{
    public int LevelId { get; set; }

    public string Name { get; set; } = null!;

    public byte? Status { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
