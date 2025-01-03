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
            return View(new StockViewModel{ Quantity = 1, ExpireDate = DateTime.Now});
        }

        [HttpPost]
        public async Task<IActionResult> CheckPZN(StockViewModel model)
        {
            // Speichere eingegebene Werte
            SaveFormValues(model);

            if (string.IsNullOrEmpty(model.PZN))
            {
                TempData["ErrorMessage"] = "Bitte geben Sie eine PZN ein.";
                return RedirectToAction(nameof(Create), model);
            }

            if (!Int64.TryParse(model.PZN, out long pzn))
            {
                TempData["ErrorMessage"] = "Die PZN ist keine reine numerische Zahl";
                return RedirectToAction(nameof(Create), model);
            }

            var medication = await _context.Medications
                .Include(m => m.MedicationGroup)
                .FirstOrDefaultAsync(m => m.PZN == Int32.Parse(model.PZN));

            if (medication == null)
            {
                TempData["ErrorMessage"] = "Medikament nicht gefunden.";
                return RedirectToAction(nameof(Create), model);
            }

            // Speichere Medikamenteninformationen
            TempData["MedicationID"] = medication.ID;
            TempData["MedicationGroupID"] = medication.MedicationGroupID;
            TempData["MedicationName"] = medication.Name;
            ViewBag.MedicationFound = true;

            return View("Create", model);
        }

        private void SaveFormValues(StockViewModel model)
        {
            // Speichere alle Formularwerte im TempData
            TempData["PZN"] = model.PZN;
            TempData["WarehouseID"] = model.WarehouseID;
            TempData["WarehouseName"] = model.WarehouseName;
            TempData["ShelfID"] = model.ShelfID;
            TempData["ShelfName"] = model.ShelfName;
            TempData["Batch"] = model.Batch;
            TempData["SerialNumber"] = model.SerialNumber;
            TempData["ExpireDate"] = model.ExpireDate;
            TempData["Quantity"] = model.Quantity;
        }

        private void RestoreFormValues(StockViewModel model)
        {
            // Stelle alle Formularwerte aus dem TempData wieder her
            if (TempData["PZN"] != null) model.PZN = TempData["PZN"].ToString();
            if (TempData["WarehouseID"] != null) model.WarehouseID = Convert.ToInt32(TempData["WarehouseID"]);
            if (TempData["WarehouseName"] != null) model.WarehouseName = TempData["WarehouseName"].ToString();
            if (TempData["ShelfID"] != null) model.ShelfID = Convert.ToInt32(TempData["ShelfID"]);
            if (TempData["ShelfName"] != null) model.ShelfName = TempData["ShelfName"].ToString();
            if (TempData["Batch"] != null) model.Batch = TempData["Batch"].ToString();
            if (TempData["SerialNumber"] != null) model.SerialNumber = TempData["SerialNumber"].ToString();
            if (TempData["ExpireDate"] != null) model.ExpireDate = Convert.ToDateTime(TempData["ExpireDate"]);
            if (TempData["Quantity"] != null) model.Quantity = Convert.ToInt32(TempData["Quantity"]);
        }

        public async Task<IActionResult> GetWarehouses(string pzn)
        {
            var medication = await _context.Medications.FirstOrDefaultAsync(n => n.PZN == Int32.Parse(pzn));
            var warehouses = await _context.WarehouseMedicationGroups.Where(n => n.MedicationGroupID == medication.MedicationGroupID).Select(w => w.Warehouse).ToListAsync();

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
            if (!string.IsNullOrEmpty(model.SerialNumber) &&
                await _context.Stocks.AnyAsync(s => s.SerialNumber == model.SerialNumber))
            {
                ModelState.AddModelError("SerialNumber", "Diese Seriennummer existiert bereits.");
                RestoreFormValues(model);
                return View(model);
            }

            if (model.ExpireDate.Date <= DateTime.Today)
            {
                ModelState.AddModelError("ExpireDate", "Das Verfallsdatum muss in der Zukunft liegen.");
                RestoreFormValues(model);
                return View(model);
            }

            var medication = await _context.Medications.FirstOrDefaultAsync(n => n.PZN == Int32.Parse(model.PZN));

            var stock = new Stock
            {
                WarehouseID = model.WarehouseID,
                MedicationGroupID = medication.MedicationGroupID,
                MedicationID = medication.ID,
                ShelfID = model.ShelfID,
                Batch = model.Batch,
                SerialNumber = model.SerialNumber,
                ExpireDate = model.ExpireDate,
                Quantity = model.Quantity
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return RedirectToAction("Create");
        }
    }
}