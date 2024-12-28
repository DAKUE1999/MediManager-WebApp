using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class MedicationController : Controller
    {
        private readonly MediManagerDbContext _context;

        public MedicationController(MediManagerDbContext context)
        {
            _context = context;
        }

        // GET: Medication
        public async Task<IActionResult> Index()
        {
            var medications = await _context.Medications
                .Include(m => m.MedicationGroup)
                .ToListAsync();
            return View(medications);
        }

        // GET: Medication/CreateEdit/5
        public async Task<IActionResult> CreateEdit(int? id)
        {
            // ViewBag für Dropdowns befüllen
            ViewData["MedicationGroups"] = new SelectList(
                await _context.MedicationGroups.OrderBy(mg => mg.Name).ToListAsync(),
                "ID",
                "Name"
            );

            if (id == null)
            {
                return View(new Medication());
            }

            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.ID == id);

            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: Medication/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("ID,MedicationGroupID,PZN,Name,Manufacturer,SAPNumber,SAPName")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                // Prüfe ob PZN bereits existiert
                if (await _context.Medications.AnyAsync(m => 
                    m.PZN == medication.PZN && m.ID != medication.ID))
                {
                    ModelState.AddModelError("PZN", "Diese PZN existiert bereits.");
                    ViewData["MedicationGroups"] = new SelectList(
                        await _context.MedicationGroups.OrderBy(mg => mg.Name).ToListAsync(),
                        "ID",
                        "Name",
                        medication.MedicationGroupID
                    );
                    return View(medication);
                }

                if (medication.ID == 0)
                {
                    _context.Add(medication);
                }
                else
                {
                    _context.Update(medication);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MedicationGroups"] = new SelectList(
                await _context.MedicationGroups.OrderBy(mg => mg.Name).ToListAsync(),
                "ID",
                "Name",
                medication.MedicationGroupID
            );
            return View(medication);
        }

        // POST: Medication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            // Prüfe ob Medikament in Verwendung ist
            var hasStock = await _context.Stocks.AnyAsync(s => s.MedicationID == id);
            if (hasStock)
            {
                TempData["ErrorMessage"] = "Das Medikament kann nicht gelöscht werden, da es noch im Bestand ist.";
                return RedirectToAction(nameof(Index));
            }

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}