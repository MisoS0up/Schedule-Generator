using System;
using System.Collections.Generic;

namespace Sched.Models.Domain;

public partial class Timeslot
{
    public int TimeslotId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public string TimeslotDisplay
    {
        get { return $"{StartTime.Value.ToString("hh:mm tt")} - {EndTime.Value.ToString("hh:mm tt")}"; }
    }
}
