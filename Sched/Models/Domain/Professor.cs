using System;
using System.Collections.Generic;

namespace Sched.Models.Domain;

public partial class Professor
{
    public int ProfessorId { get; set; }

    public string? Name { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
