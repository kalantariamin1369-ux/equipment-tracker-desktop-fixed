using System;

namespace EquipmentTracker.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string AssetTag { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // Available, In Use, Maintenance, Retired
        public string Condition { get; set; } // Excellent, Good, Fair, Poor
        public string AssignedTo { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Barcode { get; set; }
        public string QRCode { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string WarrantyExpiry { get; set; }
        public string Department { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePath { get; set; }
        
        public Equipment()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsDeleted = false;
            Status = "Available";
            Condition = "Good";
        }
    }
}
