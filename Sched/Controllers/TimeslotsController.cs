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
    public class TimeslotsController : Controller
    {
        private readonly SchedContext _context;

        public TimeslotsController(SchedContext context)
        {
            _context = context;
        }

        // GET: Timeslots
        public async Task<IActionResult> Index()
        {
              return _context.Timeslots != null ? 
                          View(await _context.Timeslots.ToListAsync()) :
                          Problem("Entity set 'SchedContext.Timeslots'  is null.");
        }

        // GET: Timeslots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Timeslots == null)
            {
                return NotFound();
            }

            var timeslot = await _context.Timeslots
                .FirstOrDefaultAsync(m => m.TimeslotId == id);
            if (timeslot == null)
            {
                return NotFound();
            }

            return View(timeslot);
        }

        // GET: Timeslots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timeslots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeslotId,StartTime,EndTime")] Timeslot timeslot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeslot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeslot);
        }

        // GET: Timeslots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Timeslots == null)
            {
                return NotFound();
            }

            var timeslot = await _context.Timeslots.FindAsync(id);
            if (timeslot == null)
            {
                return NotFound();
            }
            return View(timeslot);
        }

        // POST: Timeslots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimeslotId,StartTime,EndTime")] Timeslot timeslot)
        {
            if (id != timeslot.TimeslotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeslot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeslotExists(timeslot.TimeslotId))
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
            return View(timeslot);
        }

        // GET: Timeslots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Timeslots == null)
            {
                return NotFound();
            }

            var timeslot = await _context.Timeslots
                .FirstOrDefaultAsync(m => m.TimeslotId == id);
            if (timeslot == null)
            {
                return NotFound();
            }

            return View(timeslot);
        }

        // POST: Timeslots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Timeslots == null)
            {
                return Problem("Entity set 'SchedContext.Timeslots'  is null.");
            }
            var timeslot = await _context.Timeslots.FindAsync(id);
            if (timeslot != null)
            {
                _context.Timeslots.Remove(timeslot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeslotExists(int id)
        {
          return (_context.Timeslots?.Any(e => e.TimeslotId == id)).GetValueOrDefault();
        }
    }
}
