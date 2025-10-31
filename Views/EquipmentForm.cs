using System;
using System.Windows.Forms;
using EquipmentTracker.Models;

namespace EquipmentTracker.Views
{
    public partial class EquipmentForm : Form
    {
        public Equipment Equipment { get; private set; }
        private TextBox txtName;
        private TextBox txtType;
        private TextBox txtSerialNumber;
        private ComboBox cmbStatus;
        private DateTimePicker dtpPurchaseDate;
        private TextBox txtPrice;
        private Button btnSave;
        private Button btnCancel;
        private Label lblName;
        private Label lblType;
        private Label lblSerialNumber;
        private Label lblStatus;
        private Label lblPurchaseDate;
        private Label lblPrice;

        public EquipmentForm(Equipment equipment = null)
        {
            Equipment = equipment ?? new Equipment();
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form
            this.Text = Equipment.Id == 0 ? "Add Equipment" : "Edit Equipment";
            this.Size = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            int labelX = 20;
            int textBoxX = 140;
            int yPos = 20;
            int ySpacing = 40;
            
            // Name
            lblName = new Label();
            lblName.Text = "Name:";
            lblName.Location = new System.Drawing.Point(labelX, yPos);
            lblName.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblName);
            
            txtName = new TextBox();
            txtName.Location = new System.Drawing.Point(textBoxX, yPos);
            txtName.Size = new System.Drawing.Size(220, 20);
            this.Controls.Add(txtName);
            
            yPos += ySpacing;
            
            // Type
            lblType = new Label();
            lblType.Text = "Type:";
            lblType.Location = new System.Drawing.Point(labelX, yPos);
            lblType.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblType);
            
            txtType = new TextBox();
            txtType.Location = new System.Drawing.Point(textBoxX, yPos);
            txtType.Size = new System.Drawing.Size(220, 20);
            this.Controls.Add(txtType);
            
            yPos += ySpacing;
            
            // Serial Number
            lblSerialNumber = new Label();
            lblSerialNumber.Text = "Serial Number:";
            lblSerialNumber.Location = new System.Drawing.Point(labelX, yPos);
            lblSerialNumber.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblSerialNumber);
            
            txtSerialNumber = new TextBox();
            txtSerialNumber.Location = new System.Drawing.Point(textBoxX, yPos);
            txtSerialNumber.Size = new System.Drawing.Size(220, 20);
            this.Controls.Add(txtSerialNumber);
            
            yPos += ySpacing;
            
            // Status
            lblStatus = new Label();
            lblStatus.Text = "Status:";
            lblStatus.Location = new System.Drawing.Point(labelX, yPos);
            lblStatus.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblStatus);
            
            cmbStatus = new ComboBox();
            cmbStatus.Location = new System.Drawing.Point(textBoxX, yPos);
            cmbStatus.Size = new System.Drawing.Size(220, 20);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new object[] { "Available", "In Use", "Maintenance", "Retired" });
            this.Controls.Add(cmbStatus);
            
            yPos += ySpacing;
            
            // Purchase Date
            lblPurchaseDate = new Label();
            lblPurchaseDate.Text = "Purchase Date:";
            lblPurchaseDate.Location = new System.Drawing.Point(labelX, yPos);
            lblPurchaseDate.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblPurchaseDate);
            
            dtpPurchaseDate = new DateTimePicker();
            dtpPurchaseDate.Location = new System.Drawing.Point(textBoxX, yPos);
            dtpPurchaseDate.Size = new System.Drawing.Size(220, 20);
            dtpPurchaseDate.Format = DateTimePickerFormat.Short;
            this.Controls.Add(dtpPurchaseDate);
            
            yPos += ySpacing;
            
            // Price
            lblPrice = new Label();
            lblPrice.Text = "Price:";
            lblPrice.Location = new System.Drawing.Point(labelX, yPos);
            lblPrice.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblPrice);
            
            txtPrice = new TextBox();
            txtPrice.Location = new System.Drawing.Point(textBoxX, yPos);
            txtPrice.Size = new System.Drawing.Size(220, 20);
            this.Controls.Add(txtPrice);
            
            yPos += ySpacing + 10;
            
            // Save Button
            btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.Location = new System.Drawing.Point(180, yPos);
            btnSave.Size = new System.Drawing.Size(80, 30);
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
            
            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(280, yPos);
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);
            
            this.ResumeLayout(false);
        }

        private void LoadData()
        {
            txtName.Text = Equipment.Name;
            txtType.Text = Equipment.Type;
            txtSerialNumber.Text = Equipment.SerialNumber;
            cmbStatus.SelectedItem = Equipment.Status ?? "Available";
            dtpPurchaseDate.Value = Equipment.PurchaseDate;
            txtPrice.Text = Equipment.Price.ToString("F2");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter equipment name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Please enter equipment type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return;
            }
            
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return;
            }
            
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }
            
            Equipment.Name = txtName.Text.Trim();
            Equipment.Type = txtType.Text.Trim();
            Equipment.SerialNumber = txtSerialNumber.Text.Trim();
            Equipment.Status = cmbStatus.SelectedItem.ToString();
            Equipment.PurchaseDate = dtpPurchaseDate.Value;
            Equipment.Price = price;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
