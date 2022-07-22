using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Data.DatabaseModels;

namespace BulletJournaling.AppMVC.Controllers
{
    public class DayLogController : Controller
    {
        private readonly AppDb _context;

        public DayLogController(AppDb context)
        {
            _context = context;
        }

        // GET: DayLog
        public async Task<IActionResult> Index()
        {
              return View(await _context.DayLogs.ToListAsync());
        }

        // GET: DayLog/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DayLogs == null)
            {
                return NotFound();
            }

            var dayLog = await _context.DayLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayLog == null)
            {
                return NotFound();
            }

            return View(dayLog);
        }

        // GET: DayLog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DayLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,HasLog,day")] DayLog dayLog)
        {
            if (ModelState.IsValid)
            {
                dayLog.Id = Guid.NewGuid();
                _context.Add(dayLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dayLog);
        }

        // GET: DayLog/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DayLogs == null)
            {
                return NotFound();
            }

            var dayLog = await _context.DayLogs.FindAsync(id);
            if (dayLog == null)
            {
                return NotFound();
            }
            return View(dayLog);
        }

        // POST: DayLog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,HasLog,day")] DayLog dayLog)
        {
            if (id != dayLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dayLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayLogExists(dayLog.Id))
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
            return View(dayLog);
        }

        // GET: DayLog/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DayLogs == null)
            {
                return NotFound();
            }

            var dayLog = await _context.DayLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayLog == null)
            {
                return NotFound();
            }

            return View(dayLog);
        }

        // POST: DayLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DayLogs == null)
            {
                return Problem("Entity set 'AppDb.DayLogs'  is null.");
            }
            var dayLog = await _context.DayLogs.FindAsync(id);
            if (dayLog != null)
            {
                _context.DayLogs.Remove(dayLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayLogExists(Guid id)
        {
          return _context.DayLogs.Any(e => e.Id == id);
        }
    }
}
