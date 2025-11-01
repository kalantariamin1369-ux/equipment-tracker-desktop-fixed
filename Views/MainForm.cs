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
        private DataGridView dataGridView;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
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
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
            this.Controls.Add(dataGridView);
            
            // Button Panel
            int buttonY = 500;
            int buttonSpacing = 90;
            
            // Add Button
            btnAdd = new Button();
            btnAdd.Text = "Add";
            btnAdd.Location = new System.Drawing.Point(12, buttonY);
            btnAdd.Size = new System.Drawing.Size(80, 30);
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);
            
            // Edit Button
            btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Location = new System.Drawing.Point(12 + buttonSpacing, buttonY);
            btnEdit.Size = new System.Drawing.Size(80, 30);
            btnEdit.Click += BtnEdit_Click;
            this.Controls.Add(btnEdit);
            
            // Delete Button
            btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Location = new System.Drawing.Point(12 + buttonSpacing * 2, buttonY);
            btnDelete.Size = new System.Drawing.Size(80, 30);
            btnDelete.Click += BtnDelete_Click;
            this.Controls.Add(btnDelete);
            
            // Refresh Button
            btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new System.Drawing.Point(12 + buttonSpacing * 3, buttonY);
            btnRefresh.Size = new System.Drawing.Size(80, 30);
            btnRefresh.Click += BtnRefresh_Click;
            this.Controls.Add(btnRefresh);
            
            this.ResumeLayout(false);
        }
        private void LoadEquipment()
        {
            try
            {
                var equipment = dbManager.GetAllEquipment();
                var dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Type", typeof(string));
                dataTable.Columns.Add("Serial Number", typeof(string));
                dataTable.Columns.Add("Status", typeof(string));
                dataTable.Columns.Add("Purchase Date", typeof(DateTime));
                dataTable.Columns.Add("Price", typeof(decimal));
                
                foreach (var eq in equipment)
                {
                    dataTable.Rows.Add(eq.Id, eq.Name, eq.Type, eq.SerialNumber, eq.Status, eq.PurchaseDate, eq.Price);
                }
                
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading equipment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new EquipmentForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dbManager.AddEquipment(form.Equipment);
                        LoadEquipment();
                        MessageBox.Show("Equipment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding equipment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an equipment to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int id = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            var equipment = dbManager.GetEquipmentById(id);
            
            if (equipment != null)
            {
                using (var form = new EquipmentForm(equipment))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            dbManager.UpdateEquipment(form.Equipment);
                            LoadEquipment();
                            MessageBox.Show("Equipment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating equipment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
            MessageBox.Show("Please select an equipment to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int id = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            string name = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            
            var result = MessageBox.Show($"Are you sure you want to delete '{name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    dbManager.DeleteEquipment(id);
                    LoadEquipment();
                    MessageBox.Show("Equipment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting equipment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadEquipment();
        }
    }
}
