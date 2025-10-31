using System;
using System.Collections.Generic;
using EquipmentTracker.Models;
using EquipmentTracker.Database;

namespace EquipmentTracker.Controllers
{
    public class EquipmentController
    {
        private DatabaseManager databaseManager;

        public EquipmentController()
        {
            databaseManager = new DatabaseManager();
        }

        public List<Equipment> GetAllEquipment()
        {
            return databaseManager.GetAllEquipment();
        }

        public void AddEquipment(Equipment equipment)
        {
            databaseManager.AddEquipment(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            databaseManager.UpdateEquipment(equipment);
        }

        public void DeleteEquipment(int id)
        {
            databaseManager.DeleteEquipment(id);
        }
    }
}
