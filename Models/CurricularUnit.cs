﻿using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class CurricularUnit
{
    public int IdcurricularUnit { get; set; }

    public string Name { get; set; } = null!;

    public string Objectives { get; set; } = null!;

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();
}
