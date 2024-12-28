using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using MediManager_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class StockController : Controller
    {
        private readonly MediManagerDbContext _context;

        public StockController(MediManagerDbContext context)
        {
            _context = context;
        }

        // GET: Stock
        public async Task<IActionResult> Index(int warehouseId)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            var stocks = await _context.Stocks
                .Include(s => s.Warehouse)
                .Include(s => s.MedicationGroup)
                .Include(s => s.Medication)
                .Include(s => s.Shelf)
                .Where(s => s.WarehouseID == warehouseId)
                .Select(s => new StockViewModel
                {
                    ID = s.ID,
                    WarehouseID = s.WarehouseID,
                    WarehouseName = s.Warehouse.Name,
                    MedicationGroupID = s.MedicationGroupID,
                    MedicationGroupName = s.MedicationGroup.Name,
                    MedicationID = s.MedicationID,
                    MedicationName = s.Medication.Name,
                    ShelfID = s.ShelfID,
                    ShelfName = s.Shelf.Name,
                    Batch = s.Batch,
                    SerialNumber = s.SerialNumber,
                    ExpiryDate = s.ExpiryDate,
                    Quantity = s.Quantity
                })
                .ToListAsync();

            ViewBag.WarehouseName = warehouse.Name;
            ViewBag.WarehouseId = warehouse.ID;

            return View(stocks);
        }

        // GET: Stock/Create/5 (WarehouseID)
        public async Task<IActionResult> Create(int warehouseId)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.WarehouseMedicationGroups)
                    .ThenInclude(wmg => wmg.MedicationGroup)
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            ViewBag.WarehouseName = warehouse.Name;
            ViewBag.WarehouseId = warehouse.ID;
            ViewBag.Shelves = new SelectList(warehouse.Shelves, "ID", "Name");

            // Hole nur die Gruppen, die diesem Lager zugeordnet sind
            var availableGroups = warehouse.WarehouseMedicationGroups
                .Select(wmg => wmg.MedicationGroup)
                .OrderBy(mg => mg.Name)
                .ToList();

            ViewBag.MedicationGroups = availableGroups;
            
            return View(new StockViewModel
            {
                WarehouseID = warehouseId,
                WarehouseName = warehouse.Name,
                ExpiryDate = DateTime.Today.AddMonths(1) // Default: 1 Monat in der Zukunft
            });
        }

        // POST: Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Prüfe ob die Seriennummer bereits existiert
                if (!string.IsNullOrEmpty(model.SerialNumber) &&
                    await _context.Stocks.AnyAsync(s => s.SerialNumber == model.SerialNumber))
                {
                    ModelState.AddModelError("SerialNumber", "Diese Seriennummer existiert bereits.");
                    return await PrepareViewForError(model);
                }

                // Prüfe ob das Verfallsdatum in der Zukunft liegt
                if (model.ExpiryDate.Date <= DateTime.Today)
                {
                    ModelState.AddModelError("ExpiryDate", "Das Verfallsdatum muss in der Zukunft liegen.");
                    return await PrepareViewForError(model);
                }

                var stock = new Stock
                {
                    WarehouseID = model.WarehouseID,
                    MedicationGroupID = model.MedicationGroupID,
                    MedicationID = model.MedicationID,
                    ShelfID = model.ShelfID,
                    Batch = model.Batch,
                    SerialNumber = model.SerialNumber,
                    ExpiryDate = model.ExpiryDate,
                    Quantity = model.Quantity
                };

                _context.Add(stock);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { warehouseId = model.WarehouseID });
            }

            return await PrepareViewForError(model);
        }

        private async Task<IActionResult> PrepareViewForError(StockViewModel model)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.WarehouseMedicationGroups)
                    .ThenInclude(wmg => wmg.MedicationGroup)
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == model.WarehouseID);

            ViewBag.WarehouseName = warehouse.Name;
            ViewBag.WarehouseId = warehouse.ID;
            ViewBag.Shelves = new SelectList(warehouse.Shelves, "ID", "Name", model.ShelfID);

            var availableGroups = warehouse.WarehouseMedicationGroups
                .Select(wmg => wmg.MedicationGroup)
                .OrderBy(mg => mg.Name)
                .ToList();

            ViewBag.MedicationGroups = availableGroups;

            return View(model);
        }

        // GET: Stock/GetMedicationsForGroup/5
        public async Task<IActionResult> GetMedicationsForGroup(int groupId)
        {
            var medications = await _context.Medications
                .Where(m => m.MedicationGroupID == groupId)
                .OrderBy(m => m.Name)
                .Select(m => new MedicationSelectionViewModel
                {
                    ID = m.ID,
                    Name = m.Name,
                    PZN = m.PZN.ToString(),
                    Manufacturer = m.Manufacturer
                })
                .ToListAsync();

            return Json(medications);
        }
    }
}