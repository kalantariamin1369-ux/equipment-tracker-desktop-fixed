using System;
using System.Windows.Forms;

namespace EquipmentTracker.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Equipment Tracker";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "MainForm";
            this.Text = "Equipment Tracker";
            this.ResumeLayout(false);
        }
    }
}
