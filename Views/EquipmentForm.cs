using System.Windows.Forms;
using EquipmentTracker.Models;

namespace EquipmentTracker.Views
{
    public partial class EquipmentForm : Form
    {
        public Equipment Equipment { get; set; }

        public EquipmentForm()
        {
            InitializeComponent();
            Equipment = new Equipment();
        }

        public EquipmentForm(Equipment equipment)
        {
            InitializeComponent();
            Equipment = equipment;
        }
    }
}
