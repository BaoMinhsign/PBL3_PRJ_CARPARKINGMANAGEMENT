using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class Parking_StatusForm : Form
    {
        public Parking_StatusForm()
        {
            InitializeComponent();
            LoadcbB();
        }
        public void LoadcbB()
        {
            cartypecbB.Items.AddRange(new string[] {
                "Bãi Xe máy", "Bãi Ô tô", "Bãi Xe đạp"
            });
            cartypecbB.SelectedIndexChanged += cartypecbB_SelectedIndexChanged;
        }

        private void cartypecbB_SelectedIndexChanged(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();

            UserControl uc = null;
            string lotName = cartypecbB.SelectedItem.ToString();
            switch (cartypecbB.SelectedItem.ToString())
            {
                case "Bãi Xe máy":
                    uc = new MotorBikeParkingUC();
                    break;
                case "Bãi Ô tô":
                    uc = new CarParkingUC();
                    break;
                case "Bãi Xe đạp":
                    uc = new BikeParkingUC();
                    break;
            }

            if (uc != null)
            {
                uc.Dock = DockStyle.Fill;
                uc.BackColor = Color.Transparent;
                contentPanel.Controls.Add(uc);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

