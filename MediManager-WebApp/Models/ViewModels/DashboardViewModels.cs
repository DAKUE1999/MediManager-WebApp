namespace MediManager_WebApp.Models.ViewModels
{
    public class WarehouseDashboardViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int ShelfCount { get; set; }
        public int StockCount { get; set; }
        public int ExpiringMedicationCount { get; set; }
        public List<DashboardShelfViewModel> Shelves { get; set; } = new List<DashboardShelfViewModel>();
    }

    public class DashboardShelfViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public List<StockListViewModel> Stocks { get; set; } = new List<StockListViewModel>();
    }

    public class StockListViewModel
    {
        public int ID { get; set; }
        public string MedicationName { get; set; }
        public string Batch { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Quantity { get; set; }
    }
}