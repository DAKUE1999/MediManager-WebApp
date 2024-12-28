using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class QuantityUnitController : Controller
    {
        private readonly MediManagerDbContext _context;

        public QuantityUnitController(MediManagerDbContext context)
        {
            _context = context;
        }

        // GET: QuantityUnit
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuantityUnits.ToListAsync());
        }

        // GET: QuantityUnit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuantityUnit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] QuantityUnit quantityUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quantityUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quantityUnit);
        }

        // GET: QuantityUnit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantityUnit = await _context.QuantityUnits.FindAsync(id);
            if (quantityUnit == null)
            {
                return NotFound();
            }
            return View(quantityUnit);
        }

        // POST: QuantityUnit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] QuantityUnit quantityUnit)
        {
            if (id != quantityUnit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quantityUnit);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!QuantityUnitExists(quantityUnit.ID))
                    {
                        return NotFound();
                    } else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quantityUnit);
        }

        // GET: QuantityUnit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantityUnit = await _context.QuantityUnits
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quantityUnit == null)
            {
                return NotFound();
            }

            return View(quantityUnit);
        }

        // POST: QuantityUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quantityUnit = await _context.QuantityUnits.FindAsync(id);
            if (quantityUnit != null)
            {
                _context.QuantityUnits.Remove(quantityUnit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool QuantityUnitExists(int id)
        {
            return _context.QuantityUnits.Any(e => e.ID == id);
        }
    }
}
