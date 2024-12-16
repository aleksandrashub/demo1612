using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using Tmds.DBus.Protocol;

namespace shubenok1212.Models;

public partial class Prod
{
    public int IdProd { get; set; }

    public string? Name { get; set; }

    public float? Cost { get; set; }

    public int? IdActiv { get; set; }

    public string? Image { get; set; }

    public int? IdManuf { get; set; }

    public string? Descr { get; set; }

    public virtual Activity? IdActivNavigation { get; set; }

    public virtual Manuf? IdManufNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Prod> IdDops { get; set; } = new List<Prod>();

    public virtual ICollection<Prod> IdMains { get; set; } = new List<Prod>();

    public Bitmap bitm => Image != null && Image != "" ? new Bitmap($@"Assets\\{Image}") : null;
    public string color => IdActiv == 2 ?
        "Gray" : "White";
    public string activ => IdActiv == 2 ?
        "неактивен" : " ";
}
