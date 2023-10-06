using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mds_Core01.Models;

namespace mds_Core01.Controllers
{
    public class FormatsController : Controller
    {
        private readonly mds_Core01Context _context;

        public FormatsController(mds_Core01Context context)
        {
            _context = context;
        }

        // GET: Formats
        public async Task<IActionResult> Index()
        {
              return _context.Formats != null ? 
                          View(await _context.Formats.ToListAsync()) :
                          Problem("Entity set 'mds_Core01Context.Formats'  is null.");
        }

        // GET: Formats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formats == null)
            {
                return NotFound();
            }

            var format = await _context.Formats
                .FirstOrDefaultAsync(m => m.FormatId == id);
            if (format == null)
            {
                return NotFound();
            }

            return View(format);
        }

        // GET: Formats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormatId,FormatName")] Format format)
        {
            if (ModelState.IsValid)
            {
                _context.Add(format);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(format);
        }

        // GET: Formats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formats == null)
            {
                return NotFound();
            }

            var format = await _context.Formats.FindAsync(id);
            if (format == null)
            {
                return NotFound();
            }
            return View(format);
        }

        // POST: Formats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormatId,FormatName")] Format format)
        {
            if (id != format.FormatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(format);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormatExists(format.FormatId))
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
            return View(format);
        }

        // GET: Formats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formats == null)
            {
                return NotFound();
            }

            var format = await _context.Formats
                .FirstOrDefaultAsync(m => m.FormatId == id);
            if (format == null)
            {
                return NotFound();
            }

            return View(format);
        }

        // POST: Formats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formats == null)
            {
                return Problem("Entity set 'mds_Core01Context.Formats'  is null.");
            }
            var format = await _context.Formats.FindAsync(id);
            if (format != null)
            {
                _context.Formats.Remove(format);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormatExists(int id)
        {
          return (_context.Formats?.Any(e => e.FormatId == id)).GetValueOrDefault();
        }
    }
}
