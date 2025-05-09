using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class CarParkingUC: UserControl
    {
        public CarParkingUC()
        {
            InitializeComponent();
        }
        private void LoadParkingStatus()
        {
            var bll = new ParkingSpaceBLL();
            var ids = this.Controls
                          .OfType<Guna.UI2.WinForms.Guna2Button>()
                          .Select(b => Convert.ToInt32(b.Tag))
                          .ToList();

            var spaces = bll.GetParkingSpaceInfos("Bãi Oto");

            foreach (var btn in this.Controls.OfType<Guna.UI2.WinForms.Guna2Button>())
            {
                int id = Convert.ToInt32(btn.Tag);
                var space = spaces.FirstOrDefault(p => p.ID == id);

                if (space != null)
                {
                    if (space.Status == "Trống")
                        btn.FillColor = Color.LightGreen;
                    else
                        btn.FillColor = Color.Red;
                }
            }
        }

        private void CarParkingUC_Load(object sender, EventArgs e)
        {
            LoadParkingStatus();
        }
    }
}
