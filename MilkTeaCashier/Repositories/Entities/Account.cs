using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Account
{
    public string UserName { get; set; } = null!;

    public string DisplayedName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public int Type { get; set; }
}
