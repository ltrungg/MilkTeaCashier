using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class BillInfo
{
    public int Id { get; set; }

    public int IdBill { get; set; }

    public int IdBeverage { get; set; }

    public int Count { get; set; }

    public virtual Beverage IdBeverageNavigation { get; set; } = null!;

    public virtual Bill IdBillNavigation { get; set; } = null!;
}
