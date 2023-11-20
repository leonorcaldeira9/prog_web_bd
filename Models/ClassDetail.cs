using System;
using System.Collections.Generic;

namespace SchoolApp.Models;

public partial class ClassDetail
{
    public int IdclassDetail { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public int Teacher { get; set; }

    public int CurricularUnit { get; set; }

    public virtual CurricularUnit CurricularUnitNavigation { get; set; } = null!;

    public virtual Person TeacherNavigation { get; set; } = null!;
}
