using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class BeverageCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();
}
