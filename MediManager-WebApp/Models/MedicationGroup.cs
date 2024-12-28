using System.ComponentModel.DataAnnotations;

namespace MediManager_WebApp.Models
{
    public class MedicationGroup
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Die Versorgungsnummer ist erforderlich")]
        public string SupplyNumber { get; set; }

        [Required(ErrorMessage = "Der Name ist erforderlich")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Eine Mengeneinheit muss ausgewählt werden")]
        public int QuantityUnitID { get; set; }

        public virtual QuantityUnit QuantityUnit { get; set; }
    }
}