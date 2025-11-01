namespace EquipmentTracker.Views
{
    partial class EquipmentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblName
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Size = new System.Drawing.Size(80, 23);
            this.lblName.Text = "Name:";
            // txtName
            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Size = new System.Drawing.Size(200, 20);

            // lblType
            this.lblType.Location = new System.Drawing.Point(12, 45);
            this.lblType.Size = new System.Drawing.Size(80, 23);
            this.lblType.Text = "Type:";
            // txtType
            this.txtType.Location = new System.Drawing.Point(100, 42);
            this.txtType.Size = new System.Drawing.Size(200, 20);

            // lblSerial
            this.lblSerial.Location = new System.Drawing.Point(12, 75);
            this.lblSerial.Size = new System.Drawing.Size(80, 23);
            this.lblSerial.Text = "Serial #:";
            // txtSerial
            this.txtSerial.Location = new System.Drawing.Point(100, 72);
            this.txtSerial.Size = new System.Drawing.Size(200, 20);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(100, 110);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // EquipmentForm
            this.ClientSize = new System.Drawing.Size(320, 160);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.btnSave);
            this.Name = "EquipmentForm";
            this.Text = "Equipment";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
