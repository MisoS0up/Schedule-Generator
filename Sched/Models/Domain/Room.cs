using System;
using System.Collections.Generic;

namespace Sched.Models.Domain;

public partial class Room
{
    public int RoomId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
