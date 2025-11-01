using System;
namespace EquipmentTracker.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public Equipment()
        {
            Id = 0;
            Name = string.Empty;
            Type = string.Empty;
            SerialNumber = string.Empty;
            Status = "Available";
            PurchaseDate = DateTime.Now;
            Price = 0.0m;
        }
    }
}
