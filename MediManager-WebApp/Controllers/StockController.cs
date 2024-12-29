using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using MediManager_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Create()
        {
            return View(new StockViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CheckPZN(string pzn)
        {
            if (string.IsNullOrEmpty(pzn))
            {
                TempData["ErrorMessage"] = "Bitte geben Sie eine PZN ein.";
                return RedirectToAction(nameof(Create));
            }

            var medication = await _context.Medications
                .Include(m => m.MedicationGroup)
                .FirstOrDefaultAsync(m => m.PZN == int.Parse(pzn));

            if (medication == null)
            {
                TempData["ErrorMessage"] = "Medikament nicht gefunden.";
                return RedirectToAction(nameof(Create));
            }

            ViewBag.MedicationGroupId = medication.MedicationGroupID;
            return View("Create", new StockViewModel
            {
                PZN = int.Parse(pzn),
                MedicationID = medication.ID,
                MedicationGroupID = medication.MedicationGroupID
            });
        }

        public async Task<IActionResult> GetWarehouses(int medicationGroupId)
        {
            var warehouses = await _context.WarehouseMedicationGroups
                .Where(wmg => wmg.MedicationGroupID == medicationGroupId)
                .Select(wmg => wmg.Warehouse)
                .ToListAsync();

            return PartialView("_WarehouseSelectorModal", warehouses);
        }

        public async Task<IActionResult> GetShelves(int warehouseId)
        {
            var shelves = await _context.Shelves
                .Where(s => s.WarehouseID == warehouseId)
                .ToListAsync();

            return PartialView("_ShelfSelectorModal", shelves);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validiere Seriennummer
            if (!string.IsNullOrEmpty(model.SerialNumber) &&
                await _context.Stocks.AnyAsync(s => s.SerialNumber == model.SerialNumber))
            {
                ModelState.AddModelError("SerialNumber", "Diese Seriennummer existiert bereits.");
                return View(model);
            }

            // Validiere Verfallsdatum
            if (model.ExpireDate.Date <= DateTime.Today)
            {
                ModelState.AddModelError("ExpireDate", "Das Verfallsdatum muss in der Zukunft liegen.");
                return View(model);
            }

            var stock = new Stock
            {
                WarehouseID = model.WarehouseID,
                MedicationGroupID = model.MedicationGroupID,
                MedicationID = model.MedicationID,
                ShelfID = model.ShelfID,
                Batch = model.Batch,
                SerialNumber = model.SerialNumber,
                ExpireDate = model.ExpireDate,
                Quantity = model.Quantity
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}