using MediManager_WebApp.Database;
using MediManager_WebApp.Models;
using MediManager_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MediManagerDbContext _context;

        public HomeController(MediManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var warehouses = await _context.Warehouses
                .Include(w => w.Shelves)
                    .ThenInclude(s => s.Stocks)
                        .ThenInclude(s => s.Medication)
                .Select(w => new WarehouseDashboardViewModel
                {
                    ID = w.ID,
                    Name = w.Name,
                    Location = w.Location,
                    ShelfCount = w.Shelves.Count,
                    StockCount = w.Shelves.Sum(s => s.Stocks.Count),
                    ExpiringMedicationCount = w.Shelves
                        .SelectMany(s => s.Stocks)
                        .Count(s => (s.ExpireDate <= DateTime.Today.AddMonths(1))),
                    Shelves = w.Shelves.Select(s => new DashboardShelfViewModel
                    {
                        ID = s.ID,
                        Name = s.Name,
                        Description = s.Description,
                        StockCount = s.Stocks.Count,
                        Stocks = s.Stocks.Select(st => new StockListViewModel
                        {
                            ID = st.ID,
                            MedicationName = st.Medication.Name,
                            Batch = st.Batch,
                            ExpireDate = st.ExpireDate,
                            Quantity = st.Quantity
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return View(warehouses);
        }
    }
}