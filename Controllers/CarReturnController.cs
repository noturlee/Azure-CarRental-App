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
    public class CarReturnController : Controller
    {
        private readonly TheRideYouRentContext _context;

        public CarReturnController(TheRideYouRentContext context)
        {
            _context = context;
        }

        // GET: CarReturn
        public async Task<IActionResult> Index()
        {
            var theRideYouRentContext = _context.CarReturns.Include(c => c.CarNoNavigation).Include(c => c.Driver).Include(c => c.InspectorNoNavigation).Include(c => c.Rental);
            return View(await theRideYouRentContext.ToListAsync());
        }

        // GET: CarReturn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarReturns == null)
            {
                return NotFound();
            }

            var carReturn = await _context.CarReturns
                .Include(c => c.CarNoNavigation)
                .Include(c => c.Driver)
                .Include(c => c.InspectorNoNavigation)
                .Include(c => c.Rental)
                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (carReturn == null)
            {
                return NotFound();
            }

            return View(carReturn);
        }

        // GET: CarReturn/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name");
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalId", "RentalId");
            return View();
        }

        // POST: CarReturn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ReturnId,CarNo,RentalId,InspectorNo,DriverId,ReturnDate,ElapsedDays")] CarReturn carReturn)
        {
            // Retrieve the rental associated with the car return
            Rental rental = await _context.Rentals.FindAsync(carReturn.RentalId);

            if (rental != null)
            {
                TimeSpan rentalDuration = rental.EndDate.Date - rental.StartDate.Date;

                if (rentalDuration.TotalDays > 0)
                {
                    carReturn.Fine = carReturn.ElapsedDays * 500;
                }
                else
                {
                    carReturn.Fine = carReturn.ElapsedDays * 500;
                }
            }
            else
            {
                carReturn.Fine = carReturn.ElapsedDays * 500;
            }

            if (ModelState.IsValid)
            {
                // Save the CarReturn object to the database
                _context.CarReturns.Add(carReturn);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", carReturn.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", carReturn.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", carReturn.InspectorNo);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalId", "RentalId", carReturn.RentalId);
            return View(carReturn);
        }


        // GET: CarReturn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarReturns == null)
            {
                return NotFound();
            }

            var carReturn = await _context.CarReturns.FindAsync(id);
            if (carReturn == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", carReturn.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", carReturn.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", carReturn.InspectorNo);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalId", "RentalId", carReturn.RentalId);
            return View(carReturn);
        }

        // POST: CarReturn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnId,CarNo,RentalId,InspectorNo,DriverId,ReturnDate,ElapsedDays,Fine")] CarReturn carReturn)
        {
            if (id != carReturn.ReturnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carReturn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarReturnExists(carReturn.ReturnId))
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
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", carReturn.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", carReturn.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", carReturn.InspectorNo);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalId", "RentalId", carReturn.RentalId);
            return View(carReturn);
        }

        // GET: CarReturn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarReturns == null)
            {
                return NotFound();
            }

            var carReturn = await _context.CarReturns
                .Include(c => c.CarNoNavigation)
                .Include(c => c.Driver)
                .Include(c => c.InspectorNoNavigation)
                .Include(c => c.Rental)
                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (carReturn == null)
            {
                return NotFound();
            }

            return View(carReturn);
        }

        // POST: CarReturn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarReturns == null)
            {
                return Problem("Entity set 'TheRideYouRentContext.CarReturns'  is null.");
            }
            var carReturn = await _context.CarReturns.FindAsync(id);
            if (carReturn != null)
            {
                _context.CarReturns.Remove(carReturn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarReturnExists(int id)
        {
          return (_context.CarReturns?.Any(e => e.ReturnId == id)).GetValueOrDefault();
        }
    }
}
