using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public void LoadUC(UserControl uc)
        {
            PanelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            PanelMain.Controls.Add(uc);
        }

        private void OverView_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new OverViewUC());
        }

        private void Employee_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new EmployeeUC());
        }

        private void Customer_btn_Click(object sender, EventArgs e)
        {

        }

        private void Vehicle_btn_Click(object sender, EventArgs e)
        {

        }

        private void Parkinglot_btn_Click(object sender, EventArgs e)
        {

        }

        private void Analys_btn_Click(object sender, EventArgs e)
        {

        }

        private void Transaction_btn_Click(object sender, EventArgs e)
        {

        }

        private void Logout_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
