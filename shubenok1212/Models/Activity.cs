using System;
using System.Collections.Generic;

namespace shubenok1212.Models;

public partial class Activity
{
    public int IdActiv { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
