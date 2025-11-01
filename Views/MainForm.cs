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
            InitializeCustomComponents();
            dbManager = new DatabaseManager();
            LoadEquipment();
        }

        private void InitializeCustomComponents()
        {
            this.SuspendLayout();
            
            // MainForm
            this.Text = "Equipment Tracker";
            this.Size = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // DataGridView
            dataGridView = new DataGridView();
            dataGridView.Location = new System.Drawing.Point(12, 12);
            dataGridView.Size = new System.Drawing.Size(860, 480);
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(dataGridView);

            // Buttons Panel
            var buttonPanel = new Panel
            {
                Location = new System.Drawing.Point(12, 500),
                Size = new System.Drawing.Size(860, 50)
            };

            btnAdd = new Button
            {
                Text = "Add Equipment",
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(120, 40)
            };
            btnAdd.Click += BtnAdd_Click;
            buttonPanel.Controls.Add(btnAdd);

            btnEdit = new Button
            {
                Text = "Edit Equipment",
                Location = new System.Drawing.Point(130, 0),
                Size = new System.Drawing.Size(120, 40)
            };
            btnEdit.Click += BtnEdit_Click;
            buttonPanel.Controls.Add(btnEdit);

            btnDelete = new Button
            {
                Text = "Delete Equipment",
                Location = new System.Drawing.Point(260, 0),
                Size = new System.Drawing.Size(120, 40)
            };
            btnDelete.Click += BtnDelete_Click;
            buttonPanel.Controls.Add(btnDelete);

            btnRefresh = new Button
            {
                Text = "Refresh",
                Location = new System.Drawing.Point(390, 0),
                Size = new System.Drawing.Size(120, 40)
            };
            btnRefresh.Click += BtnRefresh_Click;
            buttonPanel.Controls.Add(btnRefresh);

            this.Controls.Add(buttonPanel);

            this.ResumeLayout(false);
        }

        private void LoadEquipment()
        {
            try
            {
                var equipment = dbManager.GetAllEquipment();
                dataGridView.DataSource = equipment;
                
                // Set column headers
                if (dataGridView.Columns.Count > 0)
                {
                    dataGridView.Columns["Id"].HeaderText = "ID";
                    dataGridView.Columns["Name"].HeaderText = "Equipment Name";
                    dataGridView.Columns["SerialNumber"].HeaderText = "Serial Number";
                    dataGridView.Columns["Location"].HeaderText = "Location";
                    dataGridView.Columns["Status"].HeaderText = "Status";
                    dataGridView.Columns["PurchaseDate"].HeaderText = "Purchase Date";
                    dataGridView.Columns["LastMaintenanceDate"].HeaderText = "Last Maintenance";
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
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an equipment to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridView.SelectedRows[0];
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
            if (dataGridView.SelectedRows.Count == 0)
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
                    var selectedRow = dataGridView.SelectedRows[0];
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
