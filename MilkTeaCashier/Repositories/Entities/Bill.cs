using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Bill
{
    public int Id { get; set; }

    public DateOnly DateCheckIn { get; set; }

    public TimeOnly EntryTime { get; set; }

    public double? TotalPrice { get; set; }

    public virtual ICollection<BillInfo> BillInfos { get; set; } = new List<BillInfo>();
}
