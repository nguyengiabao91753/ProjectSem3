using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Policy
{
    public int PolicyId { get; set; }

    public string? Title { get; set; }

    public string Content { get; set; } = null!;

    public byte? Status { get; set; }
}
