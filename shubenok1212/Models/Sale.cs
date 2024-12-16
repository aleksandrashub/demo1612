using System;
using System.Collections.Generic;

namespace shubenok1212.Models;

public partial class Sale
{
    public int IdSale { get; set; }

    public DateOnly? Date { get; set; }

    public int? IdProd { get; set; }

    public int? Amount { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual Prod? IdProdNavigation { get; set; }
}
