using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using MediManager_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class WarehouseMedicationGroupController : Controller
    {
        private readonly MediManagerDbContext _context;

        public WarehouseMedicationGroupController(MediManagerDbContext context)
        {
            _context = context;
        }

        // GET: WarehouseMedicationGroup/Index/5 (WarehouseID)
        public async Task<IActionResult> Index(int warehouseId)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.WarehouseMedicationGroups)
                    .ThenInclude(wmg => wmg.MedicationGroup)
                        .ThenInclude(mg => mg.QuantityUnit)
                .FirstOrDefaultAsync(w => w.ID == warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            ViewBag.WarehouseName = warehouse.Name;
            ViewBag.WarehouseId = warehouse.ID;
            
            return View(warehouse.WarehouseMedicationGroups);
        }

        // GET: WarehouseMedicationGroup/CreateEdit/5 (WarehouseID)
        public async Task<IActionResult> CreateEdit(int warehouseId, int? id)
        {
            // Hole alle Medikamentengruppen, die noch nicht diesem Lager zugeordnet sind
            var existingGroupIds = await _context.WarehouseMedicationGroups
                .Where(wmg => wmg.WarehouseID == warehouseId)
                .Select(wmg => wmg.MedicationGroupID)
                .ToListAsync();

            var availableGroups = await _context.MedicationGroups
                .Include(mg => mg.QuantityUnit)
                .Where(mg => !existingGroupIds.Contains(mg.ID))
                .OrderBy(mg => mg.Name)
                .Select(mg => new MedicationGroupSelectionViewModel
                {
                    ID = mg.ID,
                    Name = mg.Name,
                    SupplyNumber = mg.SupplyNumber,
                    UnitName = mg.QuantityUnit.Name
                })
                .ToListAsync();

            ViewBag.MedicationGroups = availableGroups;
            ViewBag.WarehouseId = warehouseId;
            
            if (id == null)
            {
                return View(new WarehouseMedicationGroup { WarehouseID = warehouseId });
            }

            var warehouseMedicationGroup = await _context.WarehouseMedicationGroups
                .Include(wmg => wmg.MedicationGroup)
                .FirstOrDefaultAsync(wmg => wmg.ID == id);

            if (warehouseMedicationGroup == null)
            {
                return NotFound();
            }

            return View(warehouseMedicationGroup);
        }

        // POST: WarehouseMedicationGroup/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("ID,WarehouseID,MedicationGroupID,Quantity")] WarehouseMedicationGroup warehouseMedicationGroup)
        {
            if (ModelState.IsValid)
            {
                // Prüfe ob die Kombination bereits existiert
                if (await _context.WarehouseMedicationGroups.AnyAsync(wmg => 
                    wmg.WarehouseID == warehouseMedicationGroup.WarehouseID && 
                    wmg.MedicationGroupID == warehouseMedicationGroup.MedicationGroupID && 
                    wmg.ID != warehouseMedicationGroup.ID))
                {
                    ModelState.AddModelError("", "Diese Medikamentengruppe ist bereits diesem Lager zugeordnet.");
                    return await PrepareViewForError(warehouseMedicationGroup);
                }

                if (warehouseMedicationGroup.ID == 0)
                {
                    _context.Add(warehouseMedicationGroup);
                }
                else
                {
                    _context.Update(warehouseMedicationGroup);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Warehouse");
            }

            return await PrepareViewForError(warehouseMedicationGroup);
        }

        private async Task<IActionResult> PrepareViewForError(WarehouseMedicationGroup warehouseMedicationGroup)
        {
            var existingGroupIds = await _context.WarehouseMedicationGroups
                .Where(wmg => wmg.WarehouseID == warehouseMedicationGroup.WarehouseID)
                .Select(wmg => wmg.MedicationGroupID)
                .ToListAsync();

            var availableGroups = await _context.MedicationGroups
                .Include(mg => mg.QuantityUnit)
                .Where(mg => !existingGroupIds.Contains(mg.ID))
                .OrderBy(mg => mg.Name)
                .Select(mg => new MedicationGroupSelectionViewModel
                {
                    ID = mg.ID,
                    Name = mg.Name,
                    SupplyNumber = mg.SupplyNumber,
                    UnitName = mg.QuantityUnit.Name
                })
                .ToListAsync();

            ViewBag.MedicationGroups = availableGroups;
            ViewBag.WarehouseId = warehouseMedicationGroup.WarehouseID;
            return View(warehouseMedicationGroup);
        }

        // POST: WarehouseMedicationGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouseMedicationGroup = await _context.WarehouseMedicationGroups.FindAsync(id);
            if (warehouseMedicationGroup == null)
            {
                return NotFound();
            }

            // Prüfe ob es Bestände gibt
            var hasStock = await _context.Stocks.AnyAsync(s => 
                s.WarehouseID == warehouseMedicationGroup.WarehouseID && 
                s.MedicationGroupID == warehouseMedicationGroup.MedicationGroupID);

            if (hasStock)
            {
                TempData["ErrorMessage"] = "Die Medikamentengruppe kann nicht entfernt werden, da noch Bestände vorhanden sind.";
                return RedirectToAction("Index", "Warehouse");
            }

            _context.WarehouseMedicationGroups.Remove(warehouseMedicationGroup);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Warehouse");
        }
    }
}