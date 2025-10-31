using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using EquipmentTracker.Models;

namespace EquipmentTracker.Database
{
    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager(string dbPath = "equipmenttracker.db")
        {
            connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                CreateTables(conn);
            }
        }

        private void CreateTables(SQLiteConnection conn)
        {
            string createEquipmentTable = @"
                CREATE TABLE IF NOT EXISTS Equipment (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Type TEXT NOT NULL,
                    SerialNumber TEXT,
                    Status TEXT NOT NULL,
                    PurchaseDate TEXT NOT NULL,
                    Price REAL NOT NULL
                );";

            using (var cmd = new SQLiteCommand(createEquipmentTable, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public List<Equipment> GetAllEquipment()
        {
            var equipmentList = new List<Equipment>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Equipment";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipmentList.Add(new Equipment
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Type = reader["Type"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString(),
                            Status = reader["Status"].ToString(),
                            PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"])
                        });
                    }
                }
            }

            return equipmentList;
        }

        public void AddEquipment(Equipment equipment)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Equipment (Name, Type, SerialNumber, Status, PurchaseDate, Price) 
                                 VALUES (@Name, @Type, @SerialNumber, @Status, @PurchaseDate, @Price)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name);
                    cmd.Parameters.AddWithValue("@Type", equipment.Type);
                    cmd.Parameters.AddWithValue("@SerialNumber", equipment.SerialNumber);
                    cmd.Parameters.AddWithValue("@Status", equipment.Status);
                    cmd.Parameters.AddWithValue("@PurchaseDate", equipment.PurchaseDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Price", equipment.Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEquipment(Equipment equipment)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Equipment 
                                 SET Name = @Name, Type = @Type, SerialNumber = @SerialNumber, 
                                     Status = @Status, PurchaseDate = @PurchaseDate, Price = @Price 
                                 WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", equipment.Id);
                    cmd.Parameters.AddWithValue("@Name", equipment.Name);
                    cmd.Parameters.AddWithValue("@Type", equipment.Type);
                    cmd.Parameters.AddWithValue("@SerialNumber", equipment.SerialNumber);
                    cmd.Parameters.AddWithValue("@Status", equipment.Status);
                    cmd.Parameters.AddWithValue("@PurchaseDate", equipment.PurchaseDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Price", equipment.Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEquipment(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Equipment WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
