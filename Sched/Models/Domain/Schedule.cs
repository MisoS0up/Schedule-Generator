using System;
using System.Collections.Generic;

namespace Sched.Models.Domain;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? ProfessorId { get; set; }

    public int? CourseId { get; set; }

    public int? RoomId { get; set; }

    public int? TimeslotId { get; set; }

    public int? DayId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Day? Day { get; set; }

    public virtual Professor? Professor { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Timeslot? Timeslot { get; set; }
}
