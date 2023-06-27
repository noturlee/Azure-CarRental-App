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
    public class CarMakeController : Controller
    {
        private readonly TheRideYouRentContext _context;

        public CarMakeController(TheRideYouRentContext context)
        {
            _context = context;
        }

        // GET: CarMake
        public async Task<IActionResult> Index()
        {
              return _context.CarMakes != null ? 
                          View(await _context.CarMakes.ToListAsync()) :
                          Problem("Entity set 'TheRideYouRentContext.CarMakes'  is null.");
        }

        // GET: CarMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarMakes == null)
            {
                return NotFound();
            }

            var carMake = await _context.CarMakes
                .FirstOrDefaultAsync(m => m.CarMakeId == id);
            if (carMake == null)
            {
                return NotFound();
            }

            return View(carMake);
        }

        // GET: CarMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarMakeId,Description")] CarMake carMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carMake);
        }

        // GET: CarMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarMakes == null)
            {
                return NotFound();
            }

            var carMake = await _context.CarMakes.FindAsync(id);
            if (carMake == null)
            {
                return NotFound();
            }
            return View(carMake);
        }

        // POST: CarMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarMakeId,Description")] CarMake carMake)
        {
            if (id != carMake.CarMakeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMakeExists(carMake.CarMakeId))
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
            return View(carMake);
        }

        // GET: CarMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarMakes == null)
            {
                return NotFound();
            }

            var carMake = await _context.CarMakes
                .FirstOrDefaultAsync(m => m.CarMakeId == id);
            if (carMake == null)
            {
                return NotFound();
            }

            return View(carMake);
        }

        // POST: CarMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarMakes == null)
            {
                return Problem("Entity set 'TheRideYouRentContext.CarMakes'  is null.");
            }
            var carMake = await _context.CarMakes.FindAsync(id);
            if (carMake != null)
            {
                _context.CarMakes.Remove(carMake);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarMakeExists(int id)
        {
          return (_context.CarMakes?.Any(e => e.CarMakeId == id)).GetValueOrDefault();
        }
    }
}
