using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class MedicationGroupController : Controller
    {
        private readonly MediManagerDbContext _context;

        public MedicationGroupController(MediManagerDbContext context)
        {
            _context = context;
        }

        // GET: MedicationGroup
        public async Task<IActionResult> Index()
        {
            var medicationGroups = await _context.MedicationGroups
                .Include(m => m.QuantityUnit)
                .ToListAsync();
            return View(medicationGroups);
        }

        // GET: MedicationGroup/Create
        public IActionResult Create()
        {
            ViewData["QuantityUnits"] = new SelectList(_context.QuantityUnits, "ID", "Name");
            return View();
        }

        // POST: MedicationGroup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplyNumber,Name,QuantityUnitID")] MedicationGroup medicationGroup)
        {
            if (ModelState.IsValid)
            {
                // Prüfe ob SupplyNumber bereits existiert
                if (await _context.MedicationGroups.AnyAsync(m => m.SupplyNumber == medicationGroup.SupplyNumber))
                {
                    ModelState.AddModelError("SupplyNumber", "Diese Versorgungsnummer existiert bereits.");
                    ViewData["QuantityUnits"] = new SelectList(_context.QuantityUnits, "ID", "Name", medicationGroup.QuantityUnitID);
                    return View(medicationGroup);
                }

                _context.Add(medicationGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["QuantityUnits"] = new SelectList(_context.QuantityUnits, "ID", "Name", medicationGroup.QuantityUnitID);
            return View(medicationGroup);
        }

        // GET: MedicationGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationGroup = await _context.MedicationGroups.FindAsync(id);
            if (medicationGroup == null)
            {
                return NotFound();
            }

            ViewData["QuantityUnits"] = new SelectList(_context.QuantityUnits, "ID", "Name", medicationGroup.QuantityUnitID);
            return View(medicationGroup);
        }

        // POST: MedicationGroup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SupplyNumber,Name,QuantityUnitID")] MedicationGroup medicationGroup)
        {
            if (id != medicationGroup.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Prüfe ob SupplyNumber bereits existiert (außer bei der aktuellen Gruppe)
                if (await _context.MedicationGroups.AnyAsync(m => 
                    m.SupplyNumber == medicationGroup.SupplyNumber && m.ID != medicationGroup.ID))
                {
                    ModelState.AddModelError("SupplyNumber", "Diese Versorgungsnummer existiert bereits.");
                    ViewData["QuantityUnits"] = new SelectList(_context.QuantityUnits, "ID", "Name", medicationGroup.QuantityUnitID);
                    return View(medicationGroup);
                }

                try
                {
                    _context.Update(medicationGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationGroupExists(medicationGroup.ID))
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

            ViewData["QuantityUnits"] = new SelectList(_context.QuantityUnits, "ID", "Name", medicationGroup.QuantityUnitID);
            return View(medicationGroup);
        }

        // GET: MedicationGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationGroup = await _context.MedicationGroups
                .Include(m => m.QuantityUnit)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicationGroup == null)
            {
                return NotFound();
            }

            return View(medicationGroup);
        }

        // POST: MedicationGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicationGroup = await _context.MedicationGroups.FindAsync(id);
            if (medicationGroup != null)
            {
                // Prüfe ob die Gruppe bereits verwendet wird
                var hasRelatedItems = await _context.Medications.AnyAsync(m => m.MedicationGroupID == id);
                if (hasRelatedItems)
                {
                    TempData["ErrorMessage"] = "Die Medikamentengruppe kann nicht gelöscht werden, da sie noch verwendet wird.";
                    return RedirectToAction(nameof(Index));
                }

                _context.MedicationGroups.Remove(medicationGroup);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationGroupExists(int id)
        {
            return _context.MedicationGroups.Any(e => e.ID == id);
        }
    }
}