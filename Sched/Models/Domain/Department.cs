using System;
using System.Collections.Generic;

namespace Sched.Models.Domain;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();
}
