using System;
using System.Collections.Generic;

namespace shubenok1212.Models;

public partial class Manuf
{
    public int IdManuf { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
