using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly MediManagerDbContext _context;

        public WarehouseController(MediManagerDbContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            return View(await _context.Warehouses.ToListAsync());
        }

        // GET: Warehouse/CreateEdit/5 (5 für Edit, null für Create)
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
            {
                // Create
                return PartialView("_CreateEditModal", new Warehouse());
            }

            // Edit
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return PartialView("_CreateEditModal", warehouse);
        }

        // POST: Warehouse/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("ID,Name,Description,Location")] Warehouse warehouse)
        {
            ModelState.Remove("Shelves");  // Remove virtual property from validation

            if (ModelState.IsValid)
            {
                // Prüfe ob Name bereits existiert (außer bei der aktuellen Lager bei Edit)
                if (await _context.Warehouses.AnyAsync(w => 
                    w.Name == warehouse.Name && w.ID != warehouse.ID))
                {
                    ModelState.AddModelError("Name", "Dieser Lagername existiert bereits.");
                    return PartialView("_CreateEditModal", warehouse);
                }

                if (warehouse.ID == 0)
                {
                    // Create
                    _context.Add(warehouse);
                }
                else
                {
                    // Edit
                    _context.Update(warehouse);
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_CreateEditModal", warehouse);
        }

        // POST: Warehouse/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return Json(new { success = false, message = "Lager nicht gefunden." });
            }

            // Prüfe ob das Lager verwendet wird
            var hasItems = await _context.Shelves.AnyAsync(s => s.WarehouseID == id);
            if (hasItems)
            {
                return Json(new { success = false, message = "Das Lager kann nicht gelöscht werden, da es noch Regale enthält." });
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}