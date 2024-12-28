using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using MediManager_WebApp.Models.ViewModels;
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
            var warehouses = await _context.Warehouses
                .Include(w => w.Shelves)
                .ToListAsync();
            return View(warehouses);
        }

        // GET: Warehouse/CreateEdit/5
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
            {
                // Create
                return View("CreateEdit", new WarehouseViewModel());
            }

            // Edit
            var warehouse = await _context.Warehouses
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            var viewModel = new WarehouseViewModel
            {
                ID = warehouse.ID,
                Name = warehouse.Name,
                Description = warehouse.Description,
                Location = warehouse.Location,
                Shelves = warehouse.Shelves.Select(s => new ShelfViewModel
                {
                    ID = s.ID,
                    Name = s.Name,
                    Description = s.Description
                }).ToList()
            };

            return View("CreateEdit", viewModel);
        }

        // POST: Warehouse/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(WarehouseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Prüfe ob Name bereits existiert
                if (await _context.Warehouses.AnyAsync(w => 
                    w.Name == viewModel.Name && w.ID != viewModel.ID))
                {
                    ModelState.AddModelError("Name", "Dieser Lagername existiert bereits.");
                    return View("CreateEdit", viewModel);
                }

                if (viewModel.ID == 0)
                {
                    // Create
                    var warehouse = new Warehouse
                    {
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        Location = viewModel.Location,
                        Shelves = viewModel.Shelves.Select(s => new Shelf
                        {
                            Name = s.Name,
                            Description = s.Description
                        }).ToList()
                    };
                    _context.Add(warehouse);
                }
                else
                {
                    // Edit
                    var warehouse = await _context.Warehouses
                        .Include(w => w.Shelves)
                        .FirstOrDefaultAsync(w => w.ID == viewModel.ID);

                    if (warehouse == null)
                    {
                        return NotFound();
                    }

                    // Update Warehouse properties
                    warehouse.Name = viewModel.Name;
                    warehouse.Description = viewModel.Description;
                    warehouse.Location = viewModel.Location;

                    // Update Shelves
                    var existingShelfIds = warehouse.Shelves.Select(s => s.ID).ToList();
                    var viewModelShelfIds = viewModel.Shelves.Where(s => s.ID != 0).Select(s => s.ID).ToList();

                    // Remove deleted shelves
                    var shelfsToRemove = warehouse.Shelves.Where(s => !viewModelShelfIds.Contains(s.ID)).ToList();
                    foreach (var shelf in shelfsToRemove)
                    {
                        _context.Shelves.Remove(shelf);
                    }

                    // Update existing and add new shelves
                    foreach (var shelfVM in viewModel.Shelves)
                    {
                        if (shelfVM.ID == 0)
                        {
                            // New shelf
                            warehouse.Shelves.Add(new Shelf
                            {
                                Name = shelfVM.Name,
                                Description = shelfVM.Description
                            });
                        }
                        else
                        {
                            // Update existing shelf
                            var existingShelf = warehouse.Shelves.First(s => s.ID == shelfVM.ID);
                            existingShelf.Name = shelfVM.Name;
                            existingShelf.Description = shelfVM.Description;
                        }
                    }

                    _context.Update(warehouse);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("CreateEdit", viewModel);
        }

        // POST: Warehouse/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.Shelves)
                .FirstOrDefaultAsync(w => w.ID == id);

            if (warehouse == null)
            {
                return Json(new { success = false, message = "Lager nicht gefunden." });
            }

            // Prüfe ob das Lager in Verwendung ist (z.B. in Stock)
            var hasStock = await _context.Stocks.AnyAsync(s => warehouse.Shelves.Select(sh => sh.ID).Contains(s.ShelfID));
            if (hasStock)
            {
                return Json(new { success = false, message = "Das Lager kann nicht gelöscht werden, da es noch Bestände enthält." });
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}