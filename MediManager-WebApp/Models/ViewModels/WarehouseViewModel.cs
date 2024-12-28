namespace MediManager_WebApp.Models.ViewModels
{
    public class WarehouseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public List<ShelfViewModel> Shelves { get; set; } = new List<ShelfViewModel>();
    }

    public class ShelfViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}