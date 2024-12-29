namespace MediManager_WebApp.Models.ViewModels
{
    public class StockViewModel
    {
        public int ID { get; set; }
        public int WarehouseID { get; set; }
        public int MedicationGroupID { get; set; }
        public int MedicationID { get; set; }
        public int ShelfID { get; set; }
        public int PZN { get; set; }
        public string Batch { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Quantity { get; set; }

        // Zusätzliche Properties für die UI
        public string WarehouseName { get; set; }
        public string MedicationGroupName { get; set; }
        public string MedicationName { get; set; }
        public string ShelfName { get; set; }
    }

    public class MedicationSelectionViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PZN { get; set; }
        public string Manufacturer { get; set; }
    }
}