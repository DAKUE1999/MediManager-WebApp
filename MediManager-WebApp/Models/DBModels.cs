namespace MediManager_WebApp.Models
{
    public class QuantityUnit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MedicationGroup
    {
        public int ID { get; set; }
        public int QuantityUnitID { get; set; }
        public string SupplyNumber { get; set; }
        public string Name { get; set; }

        public virtual QuantityUnit QuantityUnit { get; set; }
    }

    public class Medication
    {
        public int ID { get; set; }
        public int MedicationGroupID { get; set; }
        public int PZN { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SAPNumber { get; set; }
        public string SAPName { get; set; }

        public virtual MedicationGroup MedicationGroup { get; set; }
    }

    public class Warehouse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }

    public class WarehouseMedicationGroup
    {
        public int ID { get; set; }
        public int WarehouseID { get; set; }
        public int MedicationGroupID { get; set; }
        public int Quantity { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual MedicationGroup MedicationGroup { get; set; }
    }

    public class Shelf
    {
        public int ID { get; set; }
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }

    public class Stock
    {
        public int ID { get; set; }
        public int WarehouseID { get; set; }
        public int MedicationGroupID { get; set; }
        public int ShelfID { get; set; }
        public int MedicationID { get; set; }
        public string Batch { get; set; }
        public string SerialNumber { get; set; }
        public string ExpireDate { get; set; }
        public int Quantity { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual MedicationGroup MedicationGroup { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual Medication Medication { get; set; }
    }

    public class MovementReason
    {
        public int ID { get; set; }
        public string ReasonCode { get; set; }
        public string Description { get; set; }
        public string MovementType { get; set; }
    }

    public class StockMovement
    {
        public int ID { get; set; }
        public int WarehouseID { get; set; }
        public int MedicationGroupID { get; set; }
        public int ShelfID { get; set; }
        public int MedicationID { get; set; }
        public int StockID { get; set; }
        public int MovementReasonID { get; set; }
        public int Quantity { get; set; }
        public string Batch { get; set; }
        public string SerialNumber { get; set; }
        public string ExpireDate { get; set; }
        public string Timestamp { get; set; }
        public int UserID { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual MedicationGroup MedicationGroup { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual Medication Medication { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual MovementReason MovementReason { get; set; }
        public virtual User User { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool ForcePasswordChange { get; set; }
    }

    public class UserSetting
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public bool EmailNotifications { get; set; }
        public bool PopupNotifications { get; set; }
        public virtual User User { get; set; }
    }

    public class SystemLog
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public string Timestamp { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string ObjectType { get; set; }
        public int ObjectID { get; set; }
        public virtual User User { get; set; }
    }

    public class ExportLog
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Timestamp { get; set; }
        public string ExportType { get; set; }
        public string FileName { get; set; }
        public string FileFormat { get; set; }
        public int RecordCount { get; set; }
        public virtual User User { get; set; }
    }

    public class AuthenticationLog
    {
        public int ID { get; set; }
        public string Action { get; set; }
        public string Timestamp { get; set; }
        public string IP { get; set; }
        public string Username { get; set; }
        public bool Success { get; set; }
    }
}
