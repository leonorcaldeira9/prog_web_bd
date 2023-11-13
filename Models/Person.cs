using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchoolApp.Models;

public partial class Person
{
    [DisplayName("ID")]
    public int Idpeople { get; set; }

    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    [DisplayName("Date of Birth")]
    public DateTime BirthDate { get; set; }

    public int Roles { get; set; }

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    [DisplayName("Role")]
    public virtual Role RolesNavigation { get; set; } = null!;
}
