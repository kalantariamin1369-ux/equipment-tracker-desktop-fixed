using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EquipmentTracker.Database;
using EquipmentTracker.Models;

namespace EquipmentTracker.Views
{
    public partial class MainForm : Form
    {
        private DatabaseManager dbManager;

        public MainForm()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            try
            {
                var equipment = dbManager.GetAllEquipment();
                equipmentGrid.DataSource = equipment;
                
                // Set column headers
                if (equipmentGrid.Columns.Count > 0)
                {
                    equipmentGrid.Columns["Id"].HeaderText = "ID";
                    equipmentGrid.Columns["Name"].HeaderText = "Equipment Name";
                    equipmentGrid.Columns["SerialNumber"].HeaderText = "Serial Number";
                    equipmentGrid.Columns["Location"].HeaderText = "Location";
                    equipmentGrid.Columns["Status"].HeaderText = "Status";
                    equipmentGrid.Columns["PurchaseDate"].HeaderText = "Purchase Date";
                    equipmentGrid.Columns["LastMaintenanceDate"].HeaderText = "Last Maintenance";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading equipment: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new EquipmentForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadEquipment();
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (equipmentGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an equipment to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = equipmentGrid.SelectedRows[0];
            var equipmentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            var equipment = dbManager.GetEquipmentById(equipmentId);

            if (equipment != null)
            {
                using (var form = new EquipmentForm(equipment))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadEquipment();
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (equipmentGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an equipment to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this equipment?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var selectedRow = equipmentGrid.SelectedRows[0];
                    var equipmentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    dbManager.DeleteEquipment(equipmentId);
                    LoadEquipment();
                    MessageBox.Show("Equipment deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting equipment: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadEquipment();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbManager?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
