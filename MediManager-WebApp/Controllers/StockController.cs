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

        public async Task<IActionResult> Create(int warehouseId)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            ViewBag.WarehouseName = warehouse.Name;
            ViewBag.WarehouseId = warehouse.ID;
            ViewBag.Shelves = new SelectList(warehouse.Shelves, "ID", "Name");

            return View(new StockViewModel
            {
                WarehouseID = warehouseId,
                ExpiryDate = DateTime.Today.AddMonths(1)
            });
        }

        [HttpGet]
        public async Task<IActionResult> CheckPZN(int pzn)
        {
            var medication = await _context.Medications
                .Include(m => m.MedicationGroup)
                .FirstOrDefaultAsync(m => m.PZN == pzn);

            if (medication == null)
            {
                return Json(new { found = false });
            }

            // Prüfen ob die Gruppe dem Lager zugeordnet ist
            var hasGroup = await _context.WarehouseMedicationGroups
                .AnyAsync(wmg => wmg.WarehouseID == medication.MedicationGroupID);

            if (!hasGroup)
            {
                return Json(new { 
                    found = false,
                    errorMessage = $"Die Medikamentengruppe '{medication.MedicationGroup.Name}' ist diesem Lager noch nicht zugeordnet."
                });
            }

            return Json(new
            {
                found = true,
                medication = new
                {
                    id = medication.ID,
                    name = medication.Name,
                    manufacturer = medication.Manufacturer,
                    groupId = medication.MedicationGroupID,
                    groupName = medication.MedicationGroup.Name
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.SerialNumber) &&
                    await _context.Stocks.AnyAsync(s => s.SerialNumber == model.SerialNumber))
                {
                    ModelState.AddModelError("SerialNumber", "Diese Seriennummer existiert bereits.");
                    return await PrepareViewForError(model);
                }

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
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == model.WarehouseID);

            ViewBag.WarehouseName = warehouse.Name;
            ViewBag.WarehouseId = warehouse.ID;
            ViewBag.Shelves = new SelectList(warehouse.Shelves, "ID", "Name", model.ShelfID);

            return View(model);
        }
    }
}