using CourseRESTApp.Data;
using CourseRESTApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRESTApp.Controllers
{
    public class BikesController : Controller
    {
        private readonly BikesContext _context;

        public BikesController(BikesContext context)
        {
            _context = context;
        }

        // GET: Bikes
        public async Task<IActionResult> Index(string bikeBrand, string searchString)
        {
            IQueryable<string> brandQuery = from m in _context.Bike
                                            orderby m.Brand
                                            select m.Brand;

            var bikes = from m in _context.Bike
                        select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                bikes = bikes.Where(s => s.Model.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bikeBrand))
            {
                bikes = bikes.Where(x => x.Brand == bikeBrand);
            }

            var bikeBrandVM = new BikeBrandViewModel
            {
                Brands = new SelectList(await brandQuery.Distinct().ToListAsync()),
                Bikes = await bikes.ToListAsync()
            };

            return View(bikeBrandVM);
        }

        // GET: Bikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bike
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        // GET: Bikes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bikes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,ProdYear,WheelSize,FrameSize,Price")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bike);
        }

        // GET: Bikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bike.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }
            return View(bike);
        }

        // POST: Bikes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,ProdYear,WheelSize,FrameSize,Price")] Bike bike)
        {
            if (id != bike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeExists(bike.Id))
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
            return View(bike);
        }

        // GET: Bikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bike
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bike = await _context.Bike.FindAsync(id);
            _context.Bike.Remove(bike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikeExists(int id)
        {
            return _context.Bike.Any(e => e.Id == id);
        }
    }
}
