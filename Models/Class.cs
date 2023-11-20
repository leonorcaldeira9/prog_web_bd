using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Class
{
    public int? Idclass { get; set; }

    public int Student { get; set; }

    public virtual ClassDetail? IdclassNavigation { get; set; }

    public virtual Person StudentNavigation { get; set; } = null!;
}
