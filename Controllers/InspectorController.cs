using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THERIDEURENT1.models;

namespace THERIDEURENT1.Controllers
{
    public class InspectorController : Controller
    {
        private readonly TheRideYouRentContext _context;

        public InspectorController(TheRideYouRentContext context)
        {
            _context = context;
        }

        // GET: Inspector
        public async Task<IActionResult> Index()
        {
              return _context.Inspectors != null ? 
                          View(await _context.Inspectors.ToListAsync()) :
                          Problem("Entity set 'TheRideYouRentContext.Inspectors'  is null.");
        }

        // GET: Inspector/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Inspectors == null)
            {
                return NotFound();
            }

            var inspector = await _context.Inspectors
                .FirstOrDefaultAsync(m => m.InspectorNo == id);
            if (inspector == null)
            {
                return NotFound();
            }

            return View(inspector);
        }

        // GET: Inspector/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inspector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InspectorNo,Name,Email,Mobile")] Inspector inspector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inspector);
        }

        // GET: Inspector/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Inspectors == null)
            {
                return NotFound();
            }

            var inspector = await _context.Inspectors.FindAsync(id);
            if (inspector == null)
            {
                return NotFound();
            }
            return View(inspector);
        }

        // POST: Inspector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("InspectorNo,Name,Email,Mobile")] Inspector inspector)
        {
            if (id != inspector.InspectorNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspectorExists(inspector.InspectorNo))
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
            return View(inspector);
        }

        // GET: Inspector/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Inspectors == null)
            {
                return NotFound();
            }

            var inspector = await _context.Inspectors
                .FirstOrDefaultAsync(m => m.InspectorNo == id);
            if (inspector == null)
            {
                return NotFound();
            }

            return View(inspector);
        }

        // POST: Inspector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Inspectors == null)
            {
                return Problem("Entity set 'TheRideYouRentContext.Inspectors'  is null.");
            }
            var inspector = await _context.Inspectors.FindAsync(id);
            if (inspector != null)
            {
                _context.Inspectors.Remove(inspector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspectorExists(string id)
        {
          return (_context.Inspectors?.Any(e => e.InspectorNo == id)).GetValueOrDefault();
        }
    }
}
