using System;
using System.Windows.Forms;
using EquipmentTracker.Models;
using EquipmentTracker.Database;

namespace EquipmentTracker.Views
{
    public partial class EquipmentForm : Form
    {
        private Equipment equipment;
        private bool isEditMode;

        public EquipmentForm()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public EquipmentForm(Equipment eq) : this()
        {
            equipment = eq;
            if (eq != null)
            {
                txtName.Text = eq.Name;
                txtType.Text = eq.Type;
                txtSerial.Text = eq.SerialNumber;
                isEditMode = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Name and Type are required.");
                return;
            }

            if (equipment == null) equipment = new Equipment();
            equipment.Name = txtName.Text.Trim();
            equipment.Type = txtType.Text.Trim();
            equipment.SerialNumber = txtSerial.Text.Trim();

            var db = new DatabaseManager();

            if (isEditMode)
                db.UpdateEquipment(equipment);
            else
                db.AddEquipment(equipment);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
