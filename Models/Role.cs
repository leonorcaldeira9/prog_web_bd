using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class Role
{
    public int Idrole { get; set; }

    public string Label { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
