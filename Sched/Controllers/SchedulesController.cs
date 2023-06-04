using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sched.Models.Domain;

namespace Sched.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly SchedContext _context;

        public SchedulesController(SchedContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index(string searchString)
        {
            var schedules = _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Day)
                .Include(s => s.Professor)
                .Include(s => s.Room)
            .Include(s => s.Timeslot)
            .AsQueryable();
            //This code is for the search function.
            if (!String.IsNullOrEmpty(searchString))
            {
                schedules = schedules.Where(s =>
                    s.Course.Name.Contains(searchString) ||
                    s.Professor.Name.Contains(searchString) ||
                    s.Room.Name.Contains(searchString) ||
                    s.Day.Name.Contains(searchString) ||
                    EF.Functions.Like(s.Timeslot.StartTime.ToString(), $"%{searchString}%") ||
                    EF.Functions.Like(s.Timeslot.EndTime.ToString(), $"%{searchString}%")
                );
            }

            return View(await schedules.ToListAsync());

            //var schedContext = _context.Schedules.Include(s => s.Course).Include(s => s.Day).Include(s => s.Professor).Include(s => s.Room).Include(s => s.Timeslot);
            //return View(await schedContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Day)
                .Include(s => s.Professor)
                .Include(s => s.Room)
                .Include(s => s.Timeslot)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {


            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "Name");
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "ProfessorId", "Name");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name");
            ViewData["TimeslotId"] = new SelectList(_context.Timeslots, "TimeslotId", "TimeslotDisplay");

            // this code retrieve the highest existing ScheduleId and suggest the next one
            int nextScheduleId = _context.Schedules.Any() ? _context.Schedules.Max(s => s.ScheduleId) + 1 : 1;
            ViewBag.NextScheduleId = nextScheduleId;

            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,ProfessorId,CourseId,RoomId,TimeslotId,DayId")] Schedule schedule)
        {

            // Check if a schedule with the same TimeSlot, Day, and Room already exists
            bool isDuplicate = _context.Schedules.Any(s =>
                s.TimeslotId == schedule.TimeslotId &&
                s.DayId == schedule.DayId &&
                s.RoomId == schedule.RoomId &&
                s.ScheduleId != schedule.ScheduleId // Exclude the current schedule from the check
            );

            if (isDuplicate)
            {
                TempData["ErrorMessage"] = "Creation failed due to duplication of TimeSlot, Day, and Room.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid && !isDuplicate)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", schedule.CourseId);
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "Name", schedule.DayId);
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "ProfessorId", "Name", schedule.ProfessorId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", schedule.RoomId);
            ViewData["TimeslotId"] = new SelectList(_context.Timeslots, "TimeslotId", "TimeslotDisplay", schedule.TimeslotId);
            return View(schedule);





        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", schedule.CourseId);
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "Name", schedule.DayId);
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "ProfessorId", "Name", schedule.ProfessorId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", schedule.RoomId);
            ViewData["TimeslotId"] = new SelectList(_context.Timeslots, "TimeslotId", "TimeslotDisplay", schedule.TimeslotId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,ProfessorId,CourseId,RoomId,TimeslotId,DayId")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", schedule.CourseId);
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "Name", schedule.DayId);
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "ProfessorId", "Name", schedule.ProfessorId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", schedule.RoomId);
            ViewData["TimeslotId"] = new SelectList(_context.Timeslots, "TimeslotId", "TimeslotDisplay", schedule.TimeslotId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Day)
                .Include(s => s.Professor)
                .Include(s => s.Room)
                .Include(s => s.Timeslot)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'SchedContext.Schedules'  is null.");
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
          return (_context.Schedules?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Print(string searchQuery)
        {
            var schedules = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Day)
                .Include(s => s.Professor)
                .Include(s => s.Room)
                .Include(s => s.Timeslot)
                .Where(s => s.Professor.Name.Contains(searchQuery))
                .ToListAsync();

            if (schedules.Count == 0)
            {
                TempData["ErrorMessage"] = "Professor Name not found.";
                return RedirectToAction("Index");
            }

            var professorName = schedules.Select(s => s.Professor.Name).FirstOrDefault();
            ViewData["ProfName"] = professorName;
            var groupedSchedules = schedules.GroupBy(s => s.Timeslot.TimeslotDisplay);

            if (groupedSchedules.Any())
            {
                return View("Print", groupedSchedules);
            }
            else
            {
                TempData["ErrorMessage"] = "No schedules found for the Professor Name.";
                return RedirectToAction("Index");
            }
        }




        public async Task<IActionResult> PrintRoom(string searchQuery)
        {
            var schedules = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Day)
                .Include(s => s.Professor)
                .Include(s => s.Room)
                .Include(s => s.Timeslot)
                .Where(s => s.Room.Name.Contains(searchQuery))
                .ToListAsync();

            if (schedules.Count == 0)
            {
                TempData["ErrorMessage"] = "Room not found.";
                return RedirectToAction("Index");
            }

            var RoomName = schedules.Select(s => s.Room.Name).FirstOrDefault();
            ViewData["RoomName"] = RoomName;
            var groupedSchedules = schedules.GroupBy(s => s.Timeslot.TimeslotDisplay);

            if (groupedSchedules.Any())
            {
                return View("Print", groupedSchedules);
            }
            else
            {
                TempData["ErrorMessage"] = "No schedules found for that Room.";
                return RedirectToAction("Index");
            }
        }

    }

}