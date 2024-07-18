using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Beverage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdCategory { get; set; }

    public double Price { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<BillInfo> BillInfos { get; set; } = new List<BillInfo>();

    public virtual BeverageCategory IdCategoryNavigation { get; set; } = null!;
}
